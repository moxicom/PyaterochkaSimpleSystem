using GalaSoft.MvvmLight.Command;
using PyaterochkaSimpleSystem.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal abstract class BaseListVM<T> : ViewModelBase
    {
        // Fields
        private const string _loadingStatus = "Загрузка...";
        private T? _selectedItem;
        private bool _canReloadItems;
        private string _statusTextValue = string.Empty;
        private bool _isStatusTextVisible;
        private bool _isTableVisible;
        private ObservableCollection<T> _items;

        // Constructor
        public BaseListVM(ListTypes listType, MainWindowVM mainWindowVM)
        {
            _items = new ObservableCollection<T>();
            AddCommand = new RelayCommand(AddItem);
            RemoveCommand = new RelayCommand(RemoveItem, CanRemoveItem);
            UpdateCommand = new RelayCommand(UpdateUser, CanUpdateUser);
            ReloadItemsCommand = new RelayCommand(ReloadData, CanReloadItems);
            //if (listType == ListTypes.Products)
            //{
            //    IsStatusTextVisible = true;
            //    StatusTextValue = "PRODUCTSSSS";
            //}
        }

        // Properties
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ReloadItemsCommand { get; }
        protected MainWindowVM mainWindowVM { get; }

        public ObservableCollection<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public T? SelectedUser
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedUser));
                UpdateCommands();
            }
        }

        public bool CanReloadItems
        {
            get => _canReloadItems;
            set
            {
                _canReloadItems = value;
                OnPropertyChanged();
            }
        }

        public string StatusTextValue
        {
            get => _statusTextValue;
            set
            {
                _statusTextValue = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsStatusTextVisible
        {
            get => _isStatusTextVisible;
            set
            {
                _isStatusTextVisible = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsTableVisible
        {
            get => _isTableVisible;
            set
            {
                _isTableVisible = value;
                OnPropertyChanged();
            }
        }

        // Methods
        protected async void ReloadData()
        {
            ShowStatus(_loadingStatus);
            CanReloadItems = false;
            await LoadData();
        }

        protected async Task LoadData()
        {

            // MAKE A REQUEST TO LOAD DATA FROM DB
            ShowTable();
        }

        protected abstract void AddItem();

        protected abstract void RemoveItem();

        protected abstract void UpdateUser();

        protected void ShowStatus(string statusText)
        {
            StatusTextValue = statusText;
            IsStatusTextVisible = true;
            IsTableVisible = false;
        }

        private void ShowTable()
        {
            IsStatusTextVisible = false;
            IsTableVisible = true;
        }

        protected bool CanUpdateUser()
        {
            if (SelectedUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected bool CanRemoveItem()
        {
            if (SelectedUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void UpdateCommands()
        {

            ((RelayCommand)RemoveCommand).RaiseCanExecuteChanged();
            ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
        }
    }
}
