using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TestCommand = new RelayCommand<int>(OpenCategory);
            OpenHome();
        }

        // Properties
        public ICommand HomeBtnCommand { get; }
        public ICommand TestCommand { get; }

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
        public void OpenHome() => CurrentVM = new CategoriesVM(this);
        public void OpenCategory(int id) => CurrentVM = new ProductsVM(id, this);
    }
}
