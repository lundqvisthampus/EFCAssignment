using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Services;

public class ProductImagesService
{
    private readonly ProductImagesRepository _productImagesRepository;

    public ProductImagesService(ProductImagesRepository productImagesRepository)
    {
        _productImagesRepository = productImagesRepository;
    }

    /// <summary>
    /// Tries to add entity to DB if it doesnt already exist, using the ProductImagesRepository.
    /// </summary>
    /// <param name="imageUrl">Url of the image for the product</param>
    /// <returns>Returns entity if succeeded, else null.</returns>
    public async Task<ProductImage> CreateAsync(string imageUrl)
    {
        try
        {
            var existingProductImage = await _productImagesRepository.GetOneAsync(x => x.ImageUrl == imageUrl);
            if (existingProductImage == null)
            {
                await _productImagesRepository.CreateAsync(new ProductImage { ImageUrl = imageUrl });
                var result = await _productImagesRepository.GetOneAsync(x => x.ImageUrl == imageUrl);
                return result;
            }
            return existingProductImage;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to get entity from DB using the productImagesRepository.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>Entity if succeeded, else null</returns>
    public async Task<ProductImage> GetOneAsync(Expression<Func<ProductImage, bool>> expression)
    {
        try
        {
            var result = await _productImagesRepository.GetOneAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }


    /// <summary>
    /// Tries to get all entities from DB using the productImagesRepository.
    /// </summary>
    /// <returns>List of ProductImage if succeeded, else null.</returns>
    public async Task<IEnumerable<ProductImage>> GetAllAsync()
    {
        try
        {
            var result = await _productImagesRepository.GetAllAsync();
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
    public async Task<bool> UpdateAsync(Expression<Func<ProductImage, bool>> expression, ProductImage entity)
    {
        try
        {
            var result = await _productImagesRepository.UpdateAsync(expression, entity);
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
    public async Task<bool> DeleteAsync(Expression<Func<ProductImage, bool>> expression)
    {
        try
        {
            var result = await _productImagesRepository.GetOneAsync(expression);
            if (result != null)
            {
                await _productImagesRepository.DeleteAsync(result);
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
