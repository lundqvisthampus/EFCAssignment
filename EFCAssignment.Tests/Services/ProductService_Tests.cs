using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class ProductService_Tests
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
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productDto = new ProductDto
        {
            ProductName = "Test",
            Price = 123,
            CategoryName = "Test",
            ImageUrl = "Test"
        };

        // Act
        var result = await productService.CreateAsync(productDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneFromDatabase_ReturnEntity()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productDto = new ProductDto
        {
            ProductName = "Test",
            Price = 123,
            CategoryName = "Test",
            ImageUrl = "Test"
        };
        await productService.CreateAsync(productDto);

        // Act
        var result = await productService.GetOneAsync(x => x.ProductName == productDto.ProductName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldNotGetOneFromDatabase_ReturnNull()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        string ProductName = "Test";

        // Act
        var result = await productService.GetOneAsync(x => x.ProductName == ProductName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldtGetAllFromDatabase_ReturnList()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productDto = new ProductDto
        {
            ProductName = "Test",
            Price = 123,
            CategoryName = "Test",
            ImageUrl = "Test"
        };
        await productService.CreateAsync(productDto);

        // Act
        var result = await productService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
        Assert.NotEmpty(result);
    }


    [Fact]
    public async Task GetAllAsync_ShouldtNotGetAllFromDatabase_ReturnEmptyList()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);

        // Act
        var result = await productService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Product>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldtUpdateEntityInDb_ReturnTrue()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var productImagesRepository = new ProductImagesRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productDto = new ProductDto
        {
            ProductName = "Test",
            Price = 123,
            CategoryName = "Test",
            ImageUrl = "Test"
        };
        await productService.CreateAsync(productDto);
        var foundEntity = await productService.GetOneAsync(x => x.ProductName == "Test");
        foundEntity.ProductName = "Test2";

        // Act
        var result = await productService.UpdateAsync(x => x.Id == foundEntity.Id, foundEntity);

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
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);
        var productDto = new ProductDto
        {
            ProductName = "Test",
            Price = 123,
            CategoryName = "Test",
            ImageUrl = "Test"
        };
        await productService.CreateAsync(productDto);

        // Act
        var result = await productService.DeleteAsync(x => x.ProductName == "Test");

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
        var categoryService = new CategoryService(categoryRepository);
        var productImagesService = new ProductImagesService(productImagesRepository);
        var productService = new ProductService(productRepository, categoryService, productImagesService);

        // Act
        var result = await productService.DeleteAsync(x => x.ProductName == "Test");

        // Assert
        Assert.False(result);
    }
}