using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal class BaseListVM<T> : ViewModelBase
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
        public BaseListVM()
        {
            _items = new ObservableCollection<T>();
            AddCommand = new RelayCommand(AddItem);
            RemoveCommand = new RelayCommand(RemoveItem, CanRemoveItem);
            UpdateCommand = new RelayCommand(UpdateUser, CanUpdateUser);
            ReloadItemsCommand = new RelayCommand(ReloadData, CanReloadItems);
        }

        // Properties
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ReloadItemsCommand { get; }

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
        private async void ReloadData()
        {
            ShowStatus(_loadingStatus);
            CanReloadItems = false;
            await LoadData();
        }

        private async Task LoadData()
        {

            // MAKE A REQUEST TO LOAD DATA FROM DB
        }

        private async void AddItem()
        {
            // MAKE A REQUEST TO ADD NEW ITEM
        }

        private async void RemoveItem()
        {
            // MAKE A REQUEST TO REMOVE ITEM
        }

        private async void UpdateUser()
        {
            // MAKE A REQUEST TO UPDATE ITEM
        }

        private void ShowStatus(string statusText)
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

        private bool CanUpdateUser()
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

        private bool CanRemoveItem()
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

        private void UpdateCommands()
        {
            ((RelayCommand)RemoveCommand).RaiseCanExecuteChanged();
            ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
        }
    }
}
