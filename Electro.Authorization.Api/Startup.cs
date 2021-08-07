using Electro.Authorization.Common;
using Electro.Model.Common;
using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Electro.Model.Database.Repositories.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Electro.Authorization.Api
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
            Configuration.Bind("Authorization", new AuthorizationOptions());

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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
