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

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal class ProductsVM : BaseListVM<Product>
    {
        private readonly int _id;

        public ProductsVM(int id, MainWindowVM mainWindowVM) : base(ListTypes.Products, mainWindowVM)
        {
            _id = id;
            ItemDialogType = ItemDialogType.Product;
            ReloadData();
        }

        protected override async Task<OperationResult<bool>> RemoveDataRequest()
        {
            var service = new ProductsService();
            var result = await service.DeleteProductAsync(SelectedItem!.Id);
            return result;
        }

        protected override async Task<OperationResult<bool>> InsertDataRequest(ItemDialogData dialogData)
        {
            var service = new ProductsService();
            var product = new Product
            {
                Name = dialogData.Name,
                Amount = dialogData.Amount,
                CategoryId = _id,
                DateUpdated = DateTime.UtcNow,
            };
            var result = await service.InsertProductAsync(product);
            return result;
        }

        protected override async Task<OperationResult<ObservableCollection<Product>>> LoadDataRequest()
        {
            var service = new ProductsService();
            var result = await Task.Run( () => service.GetProductsAsync(_id));
            return result;
        }

        protected override Task<OperationResult<bool>> UpdateDataRequest()
        {
            throw new NotImplementedException();
        }
    }
}
