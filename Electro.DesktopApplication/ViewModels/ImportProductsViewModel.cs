using DevExpress.Mvvm;
using Electro.DesktopApplication.Services;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Electro.DesktopApplication.ViewModels
{
    public class ImportProductsViewModel : BindableBase
    {
        private string[] _jsonFilesPath;

        public ImportProducts ImportProducts { get; set; }

        public ImportProductsViewModel()
        {
            ImportProducts = new ImportProducts();
        }

        public ICommand BrowseFiles => new DelegateCommand(() =>
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.Multiselect = true;

            fileDialog.Title = "Выберите файлы JSON";
            fileDialog.Filter = "Json files (*.json)|*.json";

            if (fileDialog.ShowDialog() == true)
            {
                _jsonFilesPath = fileDialog.FileNames;
            }

        }, () => ImportProducts.IsRunning == false);

        public ICommand StartImport => new DelegateCommand(() =>
        {
            ImportProducts.Start(_jsonFilesPath);
        }, () => (_jsonFilesPath != null ? _jsonFilesPath.Length > 0 : false) && !ImportProducts.IsRunning);

        public ICommand StopImport => new DelegateCommand(() =>
        {
            ImportProducts.Stop();
        }, () => ImportProducts.IsRunning == true);
    }
}
