using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Services;

public class ProductReviewService
{
    private readonly ProductReviewRepository _productReviewRepository;
    private readonly UserService _userService;
    private readonly ProductService _productService;

    public ProductReviewService(ProductReviewRepository productReviewRepository, UserService userService, ProductService productService)
    {
        _productReviewRepository = productReviewRepository;
        _userService = userService;
        _productService = productService;
    }

    /// <summary>
    /// Tries to add entity to DB if it doesnt already exist.
    /// </summary>
    /// <param name="productReviewDto">Object with the data needed to create a product</param>
    /// <returns>Entity if succeeded or already exists, else null</returns>
    public async Task<ProductReview> CreateAsync(ProductReviewDto productReviewDto)
    {
        try
        {
            var existingProductReview = await _productReviewRepository.GetOneAsync(x => x.Product.ProductName == productReviewDto.ProductName && x.User.UserName == productReviewDto.UserName);
            if (existingProductReview == null)
            {
                var userEntity = await _userService.GetOneAsync(x => x.UserName == productReviewDto.UserName);
                var productEntity = await _productService.GetOneAsync(x => x.ProductName == productReviewDto.ProductName);
                var createdReview = await _productReviewRepository.CreateAsync(new ProductReview
                {
                    Rating = productReviewDto.Rating,
                    UserId = userEntity.Id,
                    ProductId = productEntity.Id
                });
                var result = await _productReviewRepository.GetOneAsync(x => x.Product.ProductName == productReviewDto.ProductName && x.User.UserName == productReviewDto.UserName);
                return result;
            }
            return existingProductReview!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to get entity from DB using the _productReviewRepository.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>Entity if succeeded, else null</returns>
    public async Task<ProductReview> GetOneAsync(Expression<Func<ProductReview, bool>> expression)
    {
        try
        {
            var result = await _productReviewRepository.GetOneAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }


    /// <summary>
    /// Tries to get all entities from DB using the _productReviewRepository.
    /// </summary>
    /// <returns>List of ProductReview if succeeded, else null.</returns>
    public async Task<IEnumerable<ProductReview>> GetAllAsync()
    {
        try
        {
            var result = await _productReviewRepository.GetAllAsync();
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
    public async Task<bool> UpdateAsync(Expression<Func<ProductReview, bool>> expression, ProductReview entity)
    {
        try
        {
            var result = await _productReviewRepository.UpdateAsync(expression, entity);
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
    public async Task<bool> DeleteAsync(Expression<Func<ProductReview, bool>> expression)
    {
        try
        {
            var result = await _productReviewRepository.GetOneAsync(expression);
            if (result != null)
            {
                await _productReviewRepository.DeleteAsync(result);
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

