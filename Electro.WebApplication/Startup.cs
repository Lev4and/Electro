using Electro.Model.Common;
using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Electro.Model.Database.Repositories.EntityFramework;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.Category;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.Characteristic;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.Manufacturer;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.Product;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.SectionsCharacteristic;
using Electro.WebApplication.Services;
using Electro.WebApplication.Services.ImageResizable;
using Electro.WebApplication.Services.ImageResizable.ImageProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
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

            services.AddTransient<IProductsSorter, DefaultProductsSorter>();
            services.AddTransient<IProductsSorter, ByAncientProductsSorter>();
            services.AddTransient<IProductsSorter, ByRecentlyProductsSorter>();
            services.AddTransient<IProductsSorter, ByPopularityProductsSorter>();
            services.AddTransient<IProductsSorter, ByAscendingNameProductsSorter>();
            services.AddTransient<IProductsSorter, ByAverageRatingProductsSorter>();
            services.AddTransient<IProductsSorter, ByAscendingPriceProductsSorter>();
            services.AddTransient<IProductsSorter, ByDescendingNameProductsSorter>();
            services.AddTransient<IProductsSorter, ByDescendingPriceProductsSorter>();

            services.AddTransient<ICategoriesSorter, DefaultCategoriesSorter>();
            services.AddTransient<ICategoriesSorter, ByAscendingNameCategoriesSorter>();
            services.AddTransient<ICategoriesSorter, ByDescendingNameCategoriesSorter>();

            services.AddTransient<ICharacteristicsSorter, DefaultCharacteristicsSorter>();
            services.AddTransient<ICharacteristicsSorter, ByAscendingNameCharacteristicsSorter>();
            services.AddTransient<ICharacteristicsSorter, ByDescendingNameCharacteristicsSorter>();

            services.AddTransient<IManufacturersSorter, DefaultManufacturersSorter>();
            services.AddTransient<IManufacturersSorter, ByAscendingNameManufacturersSorter>();
            services.AddTransient<IManufacturersSorter, ByDescendingNameManufacturersSorter>();

            services.AddTransient<ISectionsCharacteristicsSorter, DefaultSectionsCharacteristicsSorter>();
            services.AddTransient<ISectionsCharacteristicsSorter, ByAscendingNameSectionsCharacteristicsSorter>();
            services.AddTransient<ISectionsCharacteristicsSorter, ByDescendingNameSectionsCharacteristicsSorter>();

            services.AddTransient<FileService>();

            services.AddTransient<RoleManager<ApplicatonRole>>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<IRolesRepository, EFRolesRepository>();
            services.AddTransient<IUsersRepository, EFUsersRepository>();
            services.AddTransient<IProductsRepository, EFProductsRepository>();
            services.AddTransient<ICategoriesRepository, EFCategoriesRepository>();
            services.AddTransient<IProductPhotosRepository, EFProductPhotosRepository>();
            services.AddTransient<IManufacturersRepository, EFManufacturersRepository>();
            services.AddTransient<ICategoryPhotosRepository, EFCategoryPhotosRepository>();
            services.AddTransient<ICharacteristicsRepository, EFCharacteristicsRepository>();
            services.AddTransient<IProductMainPhotosRepository, EFProductMainPhotosRepository>();
            services.AddTransient<IManufacturerLogosRepository, EFManufacturerLogosRepository>();
            services.AddTransient<IProductInformationRepository, EFProductInformationRepository>();
            services.AddTransient<ISectionsCharacteristicsRepository, EFSectionsCharacteristicsRepository>();
            services.AddTransient<IManufacturerInformationRepository, EFManufacturerInformationRepository>();
            services.AddTransient<ICharacteristicCategoriesRepository, EFCharacteristicCategoriesRepository>();
            services.AddTransient<ICharacteristicCategoryValuesRepository, EFCharacteristicCategoryValuesRepository>();
            services.AddTransient<ISectionCharacteristicCategoriesRepository, EFSectionCharacteristicCategoriesRepository>();
            services.AddTransient<IProductCharacteristicCategoryValuesRepository, EFProductCharacteristicCategoryValuesRepository>();
            services.AddTransient<DataManager>();

            //services.AddTransient<IProductsRepository, ADONETProductsRepository>();
            //services.AddTransient<ICategoriesRepository, ADONETCategoriesRepository>();
            //services.AddTransient<IProductPhotosRepository, ADONETProductPhotosRepository>();
            //services.AddTransient<IManufacturersRepository, ADONETManufacturersRepository>();
            //services.AddTransient<ICharacteristicsRepository, ADONETCharacteristicsRepository>();
            //services.AddTransient<IProductMainPhotosRepository, ADONETProductMainPhotosRepository>();
            //services.AddTransient<IManufacturerLogosRepository, ADONETManufacturerLogosRepository>();
            //services.AddTransient<IProductInformationRepository, ADONETProductInformationRepository>();
            //services.AddTransient<ISectionsCharacteristicsRepository, ADONETSectionsCharacteristicsRepository>();
            //services.AddTransient<ICharacteristicCategoriesRepository, ADONETCharacteristicCategoriesRepository>();
            //services.AddTransient<ICharacteristicCategoryValuesRepository, ADONETCharacteristicCategoryValuesRepository>();
            //services.AddTransient<ISectionCharacteristicCategoriesRepository, ADONETSectionCharacteristicCategoriesRepository>();
            //services.AddTransient<IProductCharacteristicCategoryValuesRepository, ADONETProductCharacteristicCategoryValuesRepository>();
            //services.AddTransient<ImportDataManager>();

            services.AddDbContext<ElectroDbContext>((options) =>
            {
                options.UseSqlServer(DbConfig.ConnectionString);
            });

            services.Configure<FormOptions>(option =>
            {
                option.ValueCountLimit = int.MaxValue;
                option.ValueLengthLimit = int.MaxValue;
                option.KeyLengthLimit = int.MaxValue;
                option.MultipartBodyLengthLimit = int.MaxValue;
                option.MultipartBoundaryLengthLimit = int.MaxValue;
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
