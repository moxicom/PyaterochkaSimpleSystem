using PyaterochkaSimpleSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.Services
{
    internal class CategoriesService
    {
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

        public async Task<OperationResult<bool>> UpdateCategoryAsync(int categoryId, string newName)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existingCategory = await context.Categories.FindAsync(categoryId);

                    if (existingCategory != null)
                    {
                        existingCategory.Name = newName;

                        // Save changes to the database
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
