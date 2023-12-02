using PyaterochkaSimpleSystem.Enums;
using PyaterochkaSimpleSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal class CategoriesVM : BaseListVM<Category>
    {
        // Fields


        // Categories
        public CategoriesVM(MainWindowVM mainWindowVM) : base(ListTypes.Categories, mainWindowVM) 
        { 
            ReloadData();
        }

        // Properties

        // Methods

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
