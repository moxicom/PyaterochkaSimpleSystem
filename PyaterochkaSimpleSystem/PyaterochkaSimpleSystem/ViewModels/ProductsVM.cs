using PyaterochkaSimpleSystem.Enums;
using PyaterochkaSimpleSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal class ProductsVM : BaseListVM<Product>
    {

        public ProductsVM(MainWindowVM mainWindowVM) : base(ListTypes.Products, mainWindowVM)
        {
            
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
    }
}
