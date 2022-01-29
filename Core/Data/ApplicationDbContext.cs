using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Project.Configurations;
using Project.Entities;
using Project.Entities.Base;

namespace Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ExtendedUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)_httpContextAccessor.HttpContext?.User?.Identity;
                if (!claimsIdentity.IsAuthenticated)
                {
                    return "";
                }
                if (claimsIdentity != null)
                {
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                    if (claim != null)
                    {
                        return claim.Value;
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = GetUserId();
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.UpdatedAt = DateTime.Now;
                        entry.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = GetUserId();
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            modelBuilder.Entity<Province>().HasData(new Province { Id = 1, Name = "مازندران" });

            modelBuilder.Entity<City>().HasData(new City { Id = 1, Name = "ساری", ProvinceId = 1 });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<AdvertImage> AdvertImages { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }

}
