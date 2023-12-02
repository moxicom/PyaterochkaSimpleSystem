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
        private bool _homeBtnEnabled;
        private readonly CategoriesVM _categoriesVM;

        // Constructor
        public MainWindowVM()
        {
            _categoriesVM = new CategoriesVM();
            HomeBtnEnabled = false;
            HomeBtnCommand = new RelayCommand(OpenHome);
            OpenHome();
        }

        // Properties
        public ICommand HomeBtnCommand { get; }
        public bool HomeBtnEnabled 
        {
            get => _homeBtnEnabled;
            set
            {
                _homeBtnEnabled = value;
                OnPropertyChanged();
            }
        }

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
        private void OpenHome() => CurrentVM = _categoriesVM;
    }
}
