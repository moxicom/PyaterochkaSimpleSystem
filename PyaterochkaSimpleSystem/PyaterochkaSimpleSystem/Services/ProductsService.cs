using Microsoft.EntityFrameworkCore;
using PyaterochkaSimpleSystem.Models;
using PyaterochkaSimpleSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PyaterochkaSimpleSystem.Services
{
    internal class ProductsService
    {
        public async Task<OperationResult<ObservableCollection<Product>>> GetProductsAsync(int Category_id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var products = await context.Products
                        .Where(p => p.CategoryId == Category_id)
                        .ToListAsync();
                    TimeZoneInfo desiredTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                    foreach (var product in products)
                    {
                        product.DateUpdated = TimeZoneInfo.ConvertTimeFromUtc(product.DateUpdated, desiredTimeZone);
                    }
                    return OperationResult<ObservableCollection<Product>>.Success(new ObservableCollection<Product>(products));
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ObservableCollection<Product>>.Failure(ex);
            }
        }

        public async Task<OperationResult<bool>> UpdateProductAsync(Product product)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingProduct = await context.Products.FindAsync(product.Id);

                    if (existingProduct != null)
                    {
                        //MessageBox.Show(product.ShelfLife.ToString());
                        existingProduct.Name = product.Name;
                        existingProduct.Description = product.Description;
                        existingProduct.Amount = product.Amount;
                        existingProduct.ShelfLife = product.ShelfLife;
                        existingProduct.DateUpdated = DateTime.UtcNow;
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

        public async Task<OperationResult<bool>> InsertProductAsync(Product product)
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

        public async Task<OperationResult<bool>> DeleteProductAsync(int productId)
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

        public async Task<OperationResult<bool>> DeleteProductsByCategoryIdAsync(int categoryId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var productsToDelete = await context.Products
                        .Where(p => p.CategoryId == categoryId)
                        .ToListAsync();

                    context.Products.RemoveRange(productsToDelete);

                    await context.SaveChangesAsync();

                    return OperationResult<bool>.Success(true);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex);
            }
        }
    }
}
