using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using AutoMapper;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Project.Data;
using Project.Mapper;
using Project.Models;
using Project.Repositories.Infrastructure;
using Project.Services.Infrastructure;
using Project.Entities;
using Microsoft.AspNetCore.Identity;
using Project.Extentions;
using Parbad.Builder;
using Parbad.Gateway.ParbadVirtual;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Routing;
using Project.Repositories;
using Project.Services;

namespace Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region default configs
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSingleton<HtmlEncoder>(
                 HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                                                           UnicodeRanges.All}));

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "image/svg+xml" });
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddHttpContextAccessor();
            #endregion

            #region context and identity
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableSensitiveDataLogging(true);
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.UseNetTopologySuite());
            });

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddIdentity<ExtendedUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
              .AddRoleManager<RoleManager<IdentityRole>>()
              .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/account/accessdenied";
                options.Cookie.Name = "ProjectCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LogoutPath = "/account/logout";
                options.LoginPath = "/account";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            #endregion

            #region register infrastructure services
            // infrastructure service
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IFileStorageService, InAppStorageService>();
            #endregion

            #region register BL services
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new AutoMapperProfiles(geometryFactory));
            }).CreateMapper());

            services.AddSingleton<GeometryFactory>(NtsGeometryServices
                .Instance.CreateGeometryFactory(srid: 4326));

            services.AddScoped<IApplicationUserService, ApplicationUserService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<IProvincesService, ProvincesService>();
            #endregion

            #region routing
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddMvc(option =>
             {
                 option.EnableEndpointRouting = false;
                 option.Filters.Add(typeof(ModelStateCheckFilter));
                 //option.Filters.Add(typeof(VisitorFilter));
             }).AddRazorOptions(options =>
             {
                 options.ViewLocationFormats.Add("/{0}.cshtml");
             })
            .AddRazorRuntimeCompilation();
            #endregion

            #region swagger
            services.AddOurSwagger();
            services.AddSwaggerGenNewtonsoftSupport();
            #endregion

            #region cors
            services.AddCors(o =>
            {
                var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins");
                var allowedOriginsList = allowedOrigins.Split(',');

                o.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(allowedOriginsList)
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            #endregion

            #region parbad
            services.AddParbad()
                .ConfigureGateways(gateways =>
                    gateways.AddParbadVirtual()
                    .WithOptions(options =>
                        options.GatewayPath = "/MyVirtualGateway"))
                .ConfigureHttpContext(builder =>
                    builder.UseDefaultAspNetCore())
                .ConfigureStorage(builder =>
                    builder.UseMemoryCache());
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region default config
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/page", "?code={0}");

            app.UseHttpsRedirection();
            app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                context.Context.Response.Headers.Add("Cache-Control", "public, max-age=2592000")
            });

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSession();

            app.Use(async (context, next) =>
            {
                string path = context.Request.Path;

                if (path.EndsWith(".css") || path.EndsWith(".js") || path.EndsWith(".jpg") || path.EndsWith(".jpeg") || path.EndsWith(".png"))
                {
                    //Set css and js files to be cached for 7 days
                    TimeSpan maxAge = new TimeSpan(7, 0, 0, 0);     //7 days
                    context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                }
                else
                {
                    //Request for views fall here.
                    context.Response.Headers.Append("Cache-Control", "no-cache");
                    context.Response.Headers.Append("Cache-Control", "private, no-store");
                }
                await next();
            });
            #endregion

            #region swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "docs";
                c.DocumentTitle = "Zillow Api Documentation";
            });
            #endregion

            #region middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
            });

            #region parbad
            app.UseParbadVirtualGateway();
            #endregion
        }
    }
}
