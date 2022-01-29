using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Entities;

namespace Project.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ExtendedUser>
    {
        public void Configure(EntityTypeBuilder<ExtendedUser> builder)
        {
            var hasher = new PasswordHasher<ExtendedUser>();
            var adminName = Entities.Enums.Roles.Admin.ToString();
            builder.HasData(new ExtendedUser
            {
                Id = "4a9bbe2a-00df-4e89-9116-bee610e6b41b",
                Email = $"{adminName}@zillow.ir",
                NormalizedEmail = $"{adminName}@zillow.ir".Normalize(),
                EmailConfirmed = true,
                FirstName = adminName,
                LastName = "",
                UserName = adminName,
                NormalizedUserName = adminName.Normalize(),
                PasswordHash = hasher.HashPassword(null, "Z!l10w")
            });
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var adminName = Entities.Enums.Roles.Admin.ToString();
            builder.HasData(new IdentityRole
            {
                Id = "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4",
                Name = Entities.Enums.Roles.Admin.ToString(),
                NormalizedName = adminName.Normalize()
            });
            builder.HasData(new IdentityRole
            {
                Id = "a5f97684-4793-47af-af01-b1eb97433f4d",
                Name = Entities.Enums.Roles.User.ToString(),
                NormalizedName = Entities.Enums.Roles.User.ToString().Normalize()
            });
            builder.HasData(new IdentityRole
            {
                Id = "d1eed7e7-396f-45f0-9c1a-cfcfd576d609",
                Name = Entities.Enums.Roles.Operator.ToString(),
                NormalizedName = Entities.Enums.Roles.Operator.ToString().Normalize()
            });
            builder.HasData(new IdentityRole
            {
                Id = "41d4cbe4-195f-4296-b881-5a372aa7916a",
                Name = Entities.Enums.Roles.Developer.ToString(),
                NormalizedName = Entities.Enums.Roles.Developer.ToString().Normalize()
            });
        }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4",
                    UserId = "4a9bbe2a-00df-4e89-9116-bee610e6b41b"
                }
            );
        }
    }
}
