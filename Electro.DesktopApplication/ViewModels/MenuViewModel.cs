using DevExpress.Mvvm;
using Electro.DesktopApplication.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pages = Electro.DesktopApplication.Views.Pages;

namespace Electro.DesktopApplication.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly MenuPageService _menuPageService;

        public bool IsLeftDrawerOpen { get; set; }

        public Page PageSource { get; set; }

        public MenuViewModel(MenuPageService menuPageService)
        {
            _menuPageService = menuPageService;
            _menuPageService.OnPageChanged += (page) => { IsLeftDrawerOpen = false; PageSource = page; };
        }

        public ICommand OnUnchecked => new DelegateCommand(() =>
        {
            IsLeftDrawerOpen = false;
        });

        public ICommand ImportProducts => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Pages.ImportProducts.ImportProducts());
        }, () => true);

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
