using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class ProductImagesService_Tests
{
    private readonly ProductCatalogContext _context =
    new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateAsync_ShouldAddEntityToDbIfNotExist_ReturnEntity()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);
        string imageUrl = "Test";

        // Act
        var result = await productImagesService.CreateAsync(imageUrl);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductImage>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneFromDatabase_ReturnEntity()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);
        string imageUrl = "Test";
        await productImagesService.CreateAsync(imageUrl);

        // Act
        var result = await productImagesService.GetOneAsync(x => x.ImageUrl == imageUrl);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductImage>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldNotGetOneFromDatabase_ReturnNull()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);
        string imageUrl = "Test";

        // Act
        var result = await productImagesService.GetOneAsync(x => x.ImageUrl == imageUrl);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldtGetAllFromDatabase_ReturnList()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);
        string imageUrl = "Test";
        await productImagesService.CreateAsync(imageUrl);

        // Act
        var result = await productImagesService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductImage>>(result);
        Assert.NotEmpty(result);
    }


    [Fact]
    public async Task GetAllAsync_ShouldtNotGetAllFromDatabase_ReturnEmptyList()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);

        // Act
        var result = await productImagesService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductImage>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldtUpdateEntityInDb_ReturnTrue()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);
        await productImagesService.CreateAsync("Test");
        var foundEntity = await productImagesService.GetOneAsync(x => x.ImageUrl == "Test");
        foundEntity.ImageUrl = "Test2";

        // Act
        var result = await productImagesService.UpdateAsync(x => x.Id == foundEntity.Id, foundEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtRemoveEntityFromDb_ReturnTrue()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);
        await productImagesService.CreateAsync("Test");

        // Act
        var result = await productImagesService.DeleteAsync(x => x.ImageUrl == "Test");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtNotRemoveEntityFromDb_ReturnFalse()
    {
        // Arrange
        var productImagesRepository = new ProductImagesRepository(_context);
        var productImagesService = new ProductImagesService(productImagesRepository);

        // Act
        var result = await productImagesService.DeleteAsync(x => x.ImageUrl == "Test");

        // Assert
        Assert.False(result);
    }
}
