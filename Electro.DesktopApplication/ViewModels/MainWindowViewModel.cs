using DevExpress.Mvvm;
using Electro.DesktopApplication.Services;
using System.Windows.Controls;
using Pages = Electro.DesktopApplication.Views.Pages;

namespace Electro.DesktopApplication.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly PageService _pageService;

        public Page PageSource { get; set; }

        public MainWindowViewModel(PageService pageService)
        {
            _pageService = pageService;

            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new Pages.Menu.Menu());
        }
    }
}
