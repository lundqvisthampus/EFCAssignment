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

    // CREATE
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

    // READ
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

    // UPDATE
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

    // DELETE
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

