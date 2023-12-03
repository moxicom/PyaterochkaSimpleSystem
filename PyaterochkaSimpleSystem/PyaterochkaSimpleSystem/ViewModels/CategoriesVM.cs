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

        protected override async Task<OperationResult<bool>> RemoveDataRequest()
        {
            var categoriesService = new CategoriesService();
            var productsService = new ProductsService();
            var result = await productsService.DeleteProductsByCategoryIdAsync(SelectedItem!.Id);
            if (result.Error != null)
            {
                return result;
            }
            result = await categoriesService.DeleteCategoryAsync(SelectedItem!.Id);
            return result;
        }

        protected override Task<OperationResult<bool>> UpdateDataRequest()
        {
            throw new NotImplementedException();
        }

        protected override Task<OperationResult<bool>> InsertDataRequest()
        {
            throw new NotImplementedException();
        }
    }
}
