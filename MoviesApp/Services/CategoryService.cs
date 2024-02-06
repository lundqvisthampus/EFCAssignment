using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Services;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    // CREATE
    public async Task<Category> CreateAsync(string categoryName)
    {
        try
        {
            var existingCategory = await _categoryRepository.GetOneAsync(x => x.CategoryName == categoryName);
            if (existingCategory == null) 
            {
                await _categoryRepository.CreateAsync(new Category { CategoryName = categoryName });
                var result = await _categoryRepository.GetOneAsync(x => x.CategoryName == categoryName);
                return result;
            }
            return existingCategory;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    // READ
    public async Task<Category> GetOneAsync(Expression<Func<Category, bool>> expression)
    {
        try
        {
            var result = await _categoryRepository.GetOneAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        try
        {
            var result = await _categoryRepository.GetAllAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    // UPDATE
    public async Task<bool> UpdateAsync(Expression<Func<Category, bool>> expression, Category entity)
    {
        try
        {
            var result = await _categoryRepository.UpdateAsync(expression, entity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    // DELETE
    public async Task<bool> DeleteAsync(Expression<Func<Category, bool>> expression)
    {
        try
        {
            var result = await _categoryRepository.GetOneAsync(expression);
            if (result != null)
            {
                await _categoryRepository.DeleteAsync(result);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
