using PyaterochkaSimpleSystem.Enums;
using PyaterochkaSimpleSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal class ProductsVM : BaseListVM<Product>
    {
        private readonly int _id;

        public ProductsVM(int id, MainWindowVM mainWindowVM) : base(ListTypes.Products, mainWindowVM)
        {
            _id = id;
            ReloadData();
        }

        protected override void AddItem()
        {
            throw new NotImplementedException();
        }

        protected override void RemoveItem()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateUser()
        {
            throw new NotImplementedException();
        }

        protected override Task<OperationResult<ObservableCollection<Product>>> LoadDataRequest()
        {
            throw new NotImplementedException();
        }
    }
}
