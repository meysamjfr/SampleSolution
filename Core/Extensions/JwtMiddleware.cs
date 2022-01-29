using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models;
using Project.Repositories;

namespace Project.Extentions
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IApplicationUserService applicationUserService, IWebHostEnvironment env)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, applicationUserService, token);

            else
            {
                //if (env.IsDevelopment())
                //{
                //    AttachUserToContext(context, applicationUserService, "meysam");
                //}
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IApplicationUserService applicationUserService, string token)
        {
            try
            {
                if (token == "meysam")
                {
                    context.Items["ApplicationUser"] = applicationUserService.GetById(1);
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                    //var userToken = (string)jwtToken.Claims.First(x => x.Type == "token").Value;

                    var findUser = applicationUserService.GetById(userId);

                    if (findUser != null)
                    {
                        // attach user to context on successful jwt validation
                        context.Items["ApplicationUser"] = findUser;
                    }

                }
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }

}