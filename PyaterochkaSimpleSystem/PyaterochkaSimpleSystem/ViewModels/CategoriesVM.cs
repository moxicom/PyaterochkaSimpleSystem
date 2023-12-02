using PyaterochkaSimpleSystem.Enums;
using PyaterochkaSimpleSystem.Models;
using PyaterochkaSimpleSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ReloadData();
        }

        // Properties

        // Methods

        protected override async Task<OperationResult<ObservableCollection<Category>>> LoadDataRequest()
        {
            var result = await _service.GetCategoriesAsync();
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
