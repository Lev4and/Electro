using Electro.DesktopApplication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Electro.DesktopApplication.ViewModels.Locators
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();

        public MenuViewModel MenuViewModel => _provider.GetRequiredService<MenuViewModel>();

        public ImportProductsViewModel ImportProductsViewModel => _provider.GetRequiredService<ImportProductsViewModel>();

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<ImportProductsViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<WindowService>();
            services.AddTransient<MenuPageService>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}
