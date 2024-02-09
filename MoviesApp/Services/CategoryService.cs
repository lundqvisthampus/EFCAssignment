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


    /// <summary>
    /// Tries to add entity to DB if it doesnt already exist, using the categoryRepository.
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns>Returns entity if succeeded, else null.</returns>
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

    /// <summary>
    /// Tries to get entity from DB using the categoryRepository.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>Entity if succeeded, else null</returns>
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

    /// <summary>
    /// Tries to get all entities from DB using the categoryRepository.
    /// </summary>
    /// <returns>List of categories if succeeded, else null.</returns>
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

    /// <summary>
    /// Tries to update entity in DB based an an expression and entity.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id) to find entity in DB</param>
    /// <param name="entity">Entity with the new values that the entity should update to.</param>
    /// <returns>True if updated, else false.</returns>
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

    /// <summary>
    /// Tries to delete entity from DB
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>True if deleted, else false.</returns>
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
