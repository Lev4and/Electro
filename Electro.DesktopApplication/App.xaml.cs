using Electro.DesktopApplication.ViewModels.Locators;
using System.Windows;

namespace Electro.DesktopApplication
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModelLocator.Init();

            base.OnStartup(e);
        }
    }
}
