using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllersWithViews();
            //services
            //.AddMvc()
            //.AddRazorOptions(o =>
            //{
            //    o.ViewLocationFormats.Add("/Views/Shared/Components/Footer/TopWidget/OnSaleProducts/");
            //    o.ViewLocationFormats.Add("/Views/Shared/Components/Footer/TopWidget/FeaturedProducts/");
            //    o.ViewLocationFormats.Add("/Views/Shared/Components/Footer/TopWidget/TopRatedProducts/");

            //    o.ViewLocationFormats.Add("/Views/Shared/Components/Header/Topbar/Language/");
            //    o.ViewLocationFormats.Add("/Views/Shared/Components/Header/Topbar/AccountSidebarToggleButton/");
            //    o.ViewLocationFormats.Add("/Views/Shared/Components/Header/VerticalAndSecondaryMenu/SecondaryMenu/");
            //    o.ViewLocationFormats.Add("/Views/Shared/Components/Header/VerticalAndSecondaryMenu/Topbar/VerticalMenu/");

            //    o.ViewLocationFormats.Add("/Views/Home/Components/MainContent/Banner/");
            //    o.ViewLocationFormats.Add("/Views/Home/Components/MainContent/DealsAndTabs/Deal/");
            //    o.ViewLocationFormats.Add("/Views/Home/Components/MainContent/DealsAndTabs/Tabs/Featured/");
            //    o.ViewLocationFormats.Add("/Views/Home/Components/MainContent/DealsAndTabs/Tabs/OnSale/");
            //    o.ViewLocationFormats.Add("/Views/Home/Components/MainContent/DealsAndTabs/Tabs/TopRated/");
            //    o.ViewLocationFormats.Add("/Views/Home/Components/MainContent/SliderSection/");
            //});
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
