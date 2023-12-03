using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PyaterochkaSimpleSystem.ViewModels
{
    /// <summary>
    /// HomeBtnCommand
    /// HomeBtnEnabled
    /// CurrentVM
    /// </summary>
    internal class MainWindowVM : ViewModelBase
    {
        // Fields
        private ViewModelBase _currentVM;

        // Constructor
        public MainWindowVM()
        {
            HomeBtnCommand = new RelayCommand(OpenHome);
            Application.Current.MainWindow.Loaded += MainWindow_Loaded;
            OpenHome();
        }

        // Properties
        public ICommand HomeBtnCommand { get; }

        public ViewModelBase CurrentVM
        {
            get => _currentVM;
            set
            {
                if (_currentVM != value)
                {
                    _currentVM = value;
                    OnPropertyChanged();
                }
            }
        }

        // Methods

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OpenHome();
        }
        public void OpenHome() => CurrentVM = new CategoriesVM(this);
        public void OpenCategory(int id) => CurrentVM = new ProductsVM(id, this);
    }
}
