using GalaSoft.MvvmLight.Command;
using PyaterochkaSimpleSystem.Enums;
using PyaterochkaSimpleSystem.Models;
using PyaterochkaSimpleSystem.Services;
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
    internal class CategoriesVM : BaseListVM<Category>
    {
        // Fields
        private CategoriesService _service;

        // Categories
        public CategoriesVM(MainWindowVM mainWindowVM) : base(ListTypes.Categories, mainWindowVM) 
        {
            _service = new CategoriesService();
            OpenCommand = new RelayCommand(OpenCategory, CanProcessItem);
            CommandsUpdated += OnCommandsUpdated;
            ReloadData();
        }

        // Properties

        public ICommand OpenCommand { get; }

        // Methods

        private void OpenCategory()
        {
            MainWindowVM.OpenCategory(SelectedItem!.Id);
        }

        private void OnCommandsUpdated(object sender, EventArgs e)
        {
            ((RelayCommand)OpenCommand).RaiseCanExecuteChanged();
        }

        protected override async Task<OperationResult<ObservableCollection<Category>>> LoadDataRequest()
        {
            var result = await Task.Run( () => _service.GetCategoriesAsync());
            return result;
        }

        protected override async void AddItem()
        {
            throw new NotImplementedException();
        }

        protected override async void RemoveItem()
        {
            throw new NotImplementedException();
        }

        protected override async void UpdateUser()
        {
            throw new NotImplementedException();
        }

    }
}
