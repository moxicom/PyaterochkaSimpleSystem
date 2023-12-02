using Microsoft.EntityFrameworkCore;
using PyaterochkaSimpleSystem.Models;
using PyaterochkaSimpleSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.Services
{
    internal class ProductsService
    {
        public async Task<OperationResult<ObservableCollection<Product>>> GetProducts(Product product)
        {
            try
            {
                ObservableCollection<Product> products;

                using (var context = new AppDbContext())
                {
                    products = new ObservableCollection<Product>(
                        await context.Products.ToListAsync());
                }

                return OperationResult<ObservableCollection<Product>>.Success(products);
            }
            catch (Exception ex)
            {
                return OperationResult<ObservableCollection<Product>>.Failure(ex);
            }
        }

        public async Task<OperationResult<bool>> UpdateProduct(Product product)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingProduct = await context.Products.FindAsync(product.Id);

                    if (existingProduct != null)
                    {
                        existingProduct.Name = product.Name;
                        existingProduct.Description = product.Description;
                        existingProduct.DateUpdated = DateTime.Now;
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return OperationResult<bool>.Failure(new Exception("Product not found"));
                    }

                    return OperationResult<bool>.Success(true);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex);
            }
        }

        public async Task<OperationResult<bool>> InsertProduct(Product product)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Products.Add(product);

                    await context.SaveChangesAsync();
                }

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex);
            }
        }

        public async Task<OperationResult<bool>> Delete(int productId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var productToDelete = await context.Products.FindAsync(productId);

                    if (productToDelete != null)
                    {
                        context.Products.Remove(productToDelete);

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return OperationResult<bool>.Failure(new Exception("Product not found"));
                    }
                }

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex);
            }
        }
    }
}
