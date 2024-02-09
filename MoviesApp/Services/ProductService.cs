using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly CategoryService _categoryService;
    private readonly ProductImagesService _productImagesService;

    public ProductService(ProductRepository productRepository, CategoryService categoryService, ProductImagesService productImagesService)
    {
        _productRepository = productRepository;
        _categoryService = categoryService;
        _productImagesService = productImagesService;
    }

    /// <summary>
    /// Tries to add entity to DB if it doesnt already exist.
    /// </summary>
    /// <param name="productDto">Object with the data needed to create a product</param>
    /// <returns>Entity if succeeded or already exists, else null</returns>
    public async Task<Product> CreateAsync(ProductDto productDto)
    {
        try
        {
            var existingProduct = await _productRepository.GetOneAsync(x => x.ProductName == productDto.ProductName);
            if (existingProduct == null)
            {
                var category = await _categoryService.CreateAsync(productDto.CategoryName);
                var productImage = await _productImagesService.CreateAsync(productDto.ImageUrl);
                var productEntity = new Product 
                {
                    ProductName = productDto.ProductName,
                    Price = productDto.Price,
                    CategoryId = category.Id,
                    ProductImageId = productImage.Id,
                };
                await _productRepository.CreateAsync(productEntity);
                var result = await _productRepository.GetOneAsync(x => x.ProductName == productDto.ProductName);
                return result;
            }
            return existingProduct;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to get entity from DB using the _productRepository.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>Entity if succeeded, else null</returns>
    public async Task<Product> GetOneAsync(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var result = await _productRepository.GetOneAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }


    /// <summary>
    /// Tries to get all entities from DB using the _productRepository.
    /// </summary>
    /// <returns>List of Product if succeeded, else null.</returns>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var result = await _productRepository.GetAllAsync();
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
    public async Task<bool> UpdateAsync(Expression<Func<Product, bool>> expression, Product entity)
    {
        try
        {
            var result = await _productRepository.UpdateAsync(expression, entity);
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
    public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var result = await _productRepository.GetOneAsync(expression);
            if (result != null)
            {
                await _productRepository.DeleteAsync(result);
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

