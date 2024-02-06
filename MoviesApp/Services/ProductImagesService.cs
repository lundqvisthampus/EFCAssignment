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

    // CREATE
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

    // READ
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

    // UPDATE
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

    // DELETE
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
