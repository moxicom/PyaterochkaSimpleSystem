using Microsoft.EntityFrameworkCore;
using PyaterochkaSimpleSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PyaterochkaSimpleSystem.Services
{
    internal class CategoriesService
    {

        public CategoriesService()
        {

        }

        public async Task<OperationResult<ObservableCollection<Category>>> GetCategoriesAsync()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var categories = await context.Categories.ToListAsync();
                    return OperationResult<ObservableCollection<Category>>.Success(new ObservableCollection<Category>(categories));
                }
            }
            catch (Exception ex)
            {
                return OperationResult<ObservableCollection<Category>>.Failure(ex);
            }
        }

        public async Task<OperationResult<bool>> InsertCategoryAsync(Category newCategory)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Categories.Add(newCategory);

                    await context.SaveChangesAsync();
                }

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex);
            }
        }

        public async Task<OperationResult<bool>> UpdateCategoryAsync(Category category)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingCategory = await context.Categories.FindAsync(category.Id);

                    if (existingCategory!= null)
                    {
                        existingCategory.Name = category.Name;
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return OperationResult<bool>.Failure(new Exception("Category not found"));
                    }

                    return OperationResult<bool>.Success(true);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex);
            }
        }

        public async Task<OperationResult<bool>> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var categoryToDelete = await context.Categories.FindAsync(categoryId);

                    if (categoryToDelete != null)
                    {
                        context.Categories.Remove(categoryToDelete);

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return OperationResult<bool>.Failure(new Exception("Category not found"));
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
