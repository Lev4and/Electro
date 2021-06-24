using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Electro.Model.Database
{
    public class ElectroDbContext : IdentityDbContext<IdentityUser>
    {
        public ElectroDbContext(DbContextOptions<ElectroDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=DESKTOP-9CDGA5B;Database=Electro;User ID=sa;Password=sa;Trusted_Connection=True;", b => b.MigrationsAssembly("Electro.WebApplication"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "B867520A-92DB-4658-BE39-84DA53A601C0",
                Name = "Администратор",
                NormalizedName = "АДМИНИСТРАТОР"
            });

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "21F7B496-C675-4E8A-A34C-FC5EC0762FDB",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "andrey.levchenko.2001@gmail.com",
                NormalizedEmail = "ANDREY.LEVCHENKO.2001@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "B867520A-92DB-4658-BE39-84DA53A601C0",
                UserId = "21F7B496-C675-4E8A-A34C-FC5EC0762FDB"
            });
        }
    }
}
