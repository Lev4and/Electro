using Electro.Authorization.Common;
using Electro.Model.Common;
using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Electro.Model.Database.Repositories.ADONET;
using Electro.Model.Database.Repositories.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Electro.Resource.Api
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
            
            services.AddTransient<EFRolesRepository>();
            services.AddTransient<EFUsersRepository>();
            services.AddTransient<EFProductsRepository>();
            services.AddTransient<EFCategoriesRepository>();
            services.AddTransient<EFProductPhotosRepository>();
            services.AddTransient<EFManufacturersRepository>();
            services.AddTransient<EFCategoryPhotosRepository>();
            services.AddTransient<EFCharacteristicsRepository>();
            services.AddTransient<EFProductMainPhotosRepository>();
            services.AddTransient<EFManufacturerLogosRepository>();
            services.AddTransient<EFProductInformationRepository>();
            services.AddTransient<EFSectionsCharacteristicsRepository>();
            services.AddTransient<EFManufacturerInformationRepository>();
            services.AddTransient<EFCharacteristicCategoriesRepository>();
            services.AddTransient<EFCharacteristicCategoryValuesRepository>();
            services.AddTransient<EFSectionCharacteristicCategoriesRepository>();
            services.AddTransient<EFProductCharacteristicCategoryValuesRepository>();
            services.AddTransient<DataManager>(provider =>
            {
                return new DataManager(
                    provider.GetRequiredService<EFCategoriesRepository>(),
                    provider.GetRequiredService<EFCategoryPhotosRepository>(),
                    provider.GetRequiredService<EFCharacteristicCategoriesRepository>(),
                    provider.GetRequiredService<EFCharacteristicCategoryValuesRepository>(),
                    provider.GetRequiredService<EFCharacteristicsRepository>(),
                    provider.GetRequiredService<EFManufacturersRepository>(),
                    provider.GetRequiredService<EFManufacturerInformationRepository>(),
                    provider.GetRequiredService<EFManufacturerLogosRepository>(),
                    provider.GetRequiredService<EFProductCharacteristicCategoryValuesRepository>(),
                    provider.GetRequiredService<EFProductInformationRepository>(),
                    provider.GetRequiredService<EFProductMainPhotosRepository>(),
                    provider.GetRequiredService<EFProductPhotosRepository>(),
                    provider.GetRequiredService<EFProductsRepository>(),
                    provider.GetRequiredService<EFRolesRepository>(),
                    provider.GetRequiredService<EFSectionCharacteristicCategoriesRepository>(),
                    provider.GetRequiredService<EFSectionsCharacteristicsRepository>(),
                    provider.GetRequiredService<EFUsersRepository>());
            });

            services.AddTransient<ADONETProductsRepository>();
            services.AddTransient<ADONETCategoriesRepository>();
            services.AddTransient<ADONETProductPhotosRepository>();
            services.AddTransient<ADONETManufacturersRepository>();
            services.AddTransient<ADONETCharacteristicsRepository>();
            services.AddTransient<ADONETProductMainPhotosRepository>();
            services.AddTransient<ADONETManufacturerLogosRepository>();
            services.AddTransient<ADONETProductInformationRepository>();
            services.AddTransient<ADONETSectionsCharacteristicsRepository>();
            services.AddTransient<ADONETCharacteristicCategoriesRepository>();
            services.AddTransient<ADONETCharacteristicCategoryValuesRepository>();
            services.AddTransient<ADONETSectionCharacteristicCategoriesRepository>();
            services.AddTransient<ADONETProductCharacteristicCategoryValuesRepository>();
            services.AddTransient<ImportDataManager>(provider =>
            {
                return new ImportDataManager(
                    provider.GetRequiredService<ADONETCategoriesRepository>(),
                    provider.GetRequiredService<ADONETCharacteristicCategoriesRepository>(),
                    provider.GetRequiredService<ADONETCharacteristicCategoryValuesRepository>(),
                    provider.GetRequiredService<ADONETCharacteristicsRepository>(),
                    provider.GetRequiredService<ADONETManufacturersRepository>(),
                    provider.GetRequiredService<ADONETManufacturerLogosRepository>(),
                    provider.GetRequiredService<ADONETProductCharacteristicCategoryValuesRepository>(),
                    provider.GetRequiredService<ADONETProductInformationRepository>(),
                    provider.GetRequiredService<ADONETProductMainPhotosRepository>(),
                    provider.GetRequiredService<ADONETProductPhotosRepository>(),
                    provider.GetRequiredService<ADONETProductsRepository>(),
                    provider.GetRequiredService<ADONETSectionCharacteristicCategoriesRepository>(),
                    provider.GetRequiredService<ADONETSectionsCharacteristicsRepository>());
            });

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthorizationOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = AuthorizationOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = AuthorizationOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
