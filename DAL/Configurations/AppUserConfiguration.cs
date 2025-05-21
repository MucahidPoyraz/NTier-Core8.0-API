using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            var adminUser = new AppUser
            {
                Id = 1,
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            var memberUser = new AppUser
            {
                Id = 2,
                FirstName = "member",
                LastName = "user",
                UserName = "member",
                NormalizedUserName = "MEMBER",
                Email = "member@site.com",
                NormalizedEmail = "MEMBER@SITE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
            };

            memberUser.PasswordHash = hasher.HashPassword(memberUser, "Member@123");

            builder.HasData(adminUser, memberUser);
        }
    }
}
