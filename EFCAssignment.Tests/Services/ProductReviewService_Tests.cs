using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class ProductReviewService_Tests
{
    private readonly ProductCatalogContext _context =
    new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);


    [Fact]
    public async Task CreateAsync_ShouldAddEntityToDbIfNotExist_ReturnEntity()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var productReviewRepository = new ProductReviewRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productReviewService = new ProductReviewService(productReviewRepository, userService, productService);

        var testUser = new UserDto { UserName = "Test", Email = "Test@domain.com", Password = "BytMig123!"};
        await userService.CreateAsync(testUser);

        var testProduct = new ProductDto { ProductName = "Test", Price = 1, CategoryName = "Test", ImageUrl = "TestUrl"};
        await productService.CreateAsync(testProduct);

        var productReviewDto = new ProductReviewDto
        {
            UserName = "Test",
            Rating = 5,
            ProductName = "Test",
        };

        // Act
        var result = await productReviewService.CreateAsync(productReviewDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductReview>(result);
    }


    [Fact]
    public async Task GetOneAsync_ShouldGetOneFromDatabase_ReturnEntity()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var productReviewRepository = new ProductReviewRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productReviewService = new ProductReviewService(productReviewRepository, userService, productService);

        var testUser = new UserDto { UserName = "Test", Email = "Test@domain.com", Password = "BytMig123!" };
        await userService.CreateAsync(testUser);

        var testProduct = new ProductDto { ProductName = "Test", Price = 1, CategoryName = "Test", ImageUrl = "TestUrl" };
        await productService.CreateAsync(testProduct);

        var productReviewDto = new ProductReviewDto
        {
            UserName = "Test",
            Rating = 5,
            ProductName = "Test",
        };
        await productReviewService.CreateAsync(productReviewDto);

        // Act
        var result = await productReviewService.GetOneAsync(x => x.Product.ProductName == productReviewDto.UserName && x.User.UserName == productReviewDto.UserName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductReview>(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldtGetAllFromDatabase_ReturnList()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var productReviewRepository = new ProductReviewRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productReviewService = new ProductReviewService(productReviewRepository, userService, productService);

        var testUser = new UserDto { UserName = "Test", Email = "Test@domain.com", Password = "BytMig123!" };
        await userService.CreateAsync(testUser);

        var testProduct = new ProductDto { ProductName = "Test", Price = 1, CategoryName = "Test", ImageUrl = "TestUrl" };
        await productService.CreateAsync(testProduct);

        var productReviewDto = new ProductReviewDto
        {
            UserName = "Test",
            Rating = 5,
            ProductName = "Test",
        };
        await productReviewService.CreateAsync(productReviewDto);

        // Act
        var result = await productReviewService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductReview>>(result);
        Assert.NotEmpty(result);
    }


    [Fact]
    public async Task GetAllAsync_ShouldtNotGetAllFromDatabase_ReturnEmptyList()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var productReviewRepository = new ProductReviewRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productReviewService = new ProductReviewService(productReviewRepository, userService, productService);

        // Act
        var result = await productReviewService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductReview>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldtUpdateEntityInDb_ReturnTrue()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var productReviewRepository = new ProductReviewRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productReviewService = new ProductReviewService(productReviewRepository, userService, productService);

        var testUser = new UserDto { UserName = "Test", Email = "Test@domain.com", Password = "BytMig123!" };
        await userService.CreateAsync(testUser);

        var testProduct = new ProductDto { ProductName = "Test", Price = 1, CategoryName = "Test", ImageUrl = "TestUrl" };
        await productService.CreateAsync(testProduct);

        var productReviewDto = new ProductReviewDto
        {
            UserName = "Test",
            Rating = 5,
            ProductName = "Test",
        };
        await productReviewService.CreateAsync(productReviewDto);
        var foundEntity = await productReviewService.GetOneAsync(x => x.Product.ProductName == productReviewDto.UserName && x.User.UserName == productReviewDto.UserName);
        foundEntity.Rating = 3;

        // Act
        var result = await productReviewService.UpdateAsync(x => x.Id == foundEntity.Id, foundEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtRemoveEntityFromDb_ReturnTrue()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var productReviewRepository = new ProductReviewRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productReviewService = new ProductReviewService(productReviewRepository, userService, productService);

        var testUser = new UserDto { UserName = "Test", Email = "Test@domain.com", Password = "BytMig123!" };
        await userService.CreateAsync(testUser);

        var testProduct = new ProductDto { ProductName = "Test", Price = 1, CategoryName = "Test", ImageUrl = "TestUrl" };
        await productService.CreateAsync(testProduct);

        var productReviewDto = new ProductReviewDto
        {
            UserName = "Test",
            Rating = 5,
            ProductName = "Test",
        };
        await productReviewService.CreateAsync(productReviewDto);

        // Act
        var result = await productReviewService.DeleteAsync(x => x.Product.ProductName == productReviewDto.UserName && x.User.UserName == productReviewDto.UserName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtNotRemoveEntityFromDb_ReturnFalse()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var productReviewRepository = new ProductReviewRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productReviewService = new ProductReviewService(productReviewRepository, userService, productService);

        // Act
        var result = await productReviewService.DeleteAsync(x => x.UserId == 0);

        // Assert
        Assert.False(result);
    }
}