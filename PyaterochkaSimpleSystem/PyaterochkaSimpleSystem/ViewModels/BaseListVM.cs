﻿using GalaSoft.MvvmLight.Command;
using PyaterochkaSimpleSystem.Enums;
using PyaterochkaSimpleSystem.Models;
using PyaterochkaSimpleSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal abstract class BaseListVM<T> : ViewModelBase
    {
        // Fields
        private const string _loadingStatus = "Загрузка...";
        private const string _requestErrorStatus = "Произошла ошибка при взаимодействии с базой данных";
        private T? _selectedItem;
        private bool _canReloadItems;
        private string _statusTextValue = string.Empty;
        private bool _isStatusTextVisible;
        private bool _isTableVisible;
        private ObservableCollection<T> _items;

        protected event EventHandler CommandsUpdated;

        // Constructor
        public BaseListVM(ListTypes listType, MainWindowVM mainWindowVM)
        {
            MainWindowVM = mainWindowVM;
            _items = new ObservableCollection<T>();
            AddCommand = new RelayCommand(AddItem);
            RemoveCommand = new RelayCommand(RemoveItem, CanProcessItem);
            UpdateCommand = new RelayCommand(UpdateItem, CanProcessItem);
            ReloadItemsCommand = new RelayCommand(ReloadData, CanReloadItems);
        }

        // Properties
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ReloadItemsCommand { get; }
        protected MainWindowVM MainWindowVM { get; }

        public ObservableCollection<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public T? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
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
            Items = null;
            CanReloadItems = false;
            LoadData();
        }

        protected async void LoadData()
        {
            ShowStatus(_loadingStatus);
            var result = await LoadDataRequest();
            if (result.Error != null)
            {
                ShowStatus(_requestErrorStatus);
                return;
            }
            Items = result.Result;
            ShowTable();
            CanReloadItems = true;
        }

        protected async void AddItem()
        {

        }

        protected async void RemoveItem()
        {
            var confirmationDialog = new ConfirmationDialog();
            var message = $"Вы уверены, что хотите удалить выбранный объект?";
            if (await confirmationDialog.ShowConfirmationDialog(message) == false)
                return;
            ShowStatus(_loadingStatus);
            var result = await RemoveDataRequest();
            if (result.Error != null)
            {
                MessageBox.Show("Произошла ошибка удаления");
            }
            ReloadData();
        }

        protected async void UpdateItem()
        {

        }

        protected abstract Task<OperationResult<ObservableCollection<T>>> LoadDataRequest();
        protected abstract Task<OperationResult<bool>> RemoveDataRequest();
        protected abstract Task<OperationResult<bool>> UpdateDataRequest();
        protected abstract Task<OperationResult<bool>> InsertDataRequest();

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

        protected bool CanProcessItem()
        {
            if (SelectedItem == null)
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

            CommandsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
