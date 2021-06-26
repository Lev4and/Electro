using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Electro.Model.Database.Repositories.EntityFramework;
using Electro.WebApplication.Services;
using Electro.WebApplication.Services.ImageResizable;
using Electro.WebApplication.Services.ImageResizable.ImageProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Electro.WebApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("DbConfig", new DbConfig());

            services.AddTransient<IImageProfile, BoxImageProfile>();
            services.AddTransient<IImageProfile, LogoImageProfile>();
            services.AddTransient<ImageService>();

            services.AddTransient<FileService>();

            services.AddTransient<RoleManager<ApplicatonRole>>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<IRolesRepository, EFRolesRepository>();
            services.AddTransient<IUsersRepository, EFUsersRepository>();
            services.AddTransient<ICategoriesRepository, EFCategoriesRepository>();
            services.AddTransient<ICategoryPhotosRepository, EFCategoryPhotosRepository>();
            services.AddTransient<DataManager>();
            services.AddDbContext<ElectroDbContext>((options) =>
            {
                options.UseSqlServer(DbConfig.ConnectionString);
            });

            services.AddIdentity<ApplicationUser, ApplicatonRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ElectroDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "fitnessCenterAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("Администратор"); });
                x.AddPolicy("ClientArea", policy => { policy.RequireRole("Клиент"); });
            });

            services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AreaAuthorization("Admin", "AdminArea"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
