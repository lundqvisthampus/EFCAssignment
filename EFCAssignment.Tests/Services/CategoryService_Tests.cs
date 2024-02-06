using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class CategoryService_Tests
{
    private readonly ProductCatalogContext _context =
    new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateAsync_ShouldAddEntityToDbIfNotExist_ReturnEntity()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test";

        // Act
        var result = await categoryService.CreateAsync(categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Category>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneFromDatabase_ReturnEntity()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test";
        await categoryService.CreateAsync(categoryName);

        // Act
        var result = await categoryService.GetOneAsync(x => x.CategoryName == categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Category>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldNotGetOneFromDatabase_ReturnNull()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test";

        // Act
        var result = await categoryService.GetOneAsync(x => x.CategoryName == categoryName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldtGetAllFromDatabase_ReturnList()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        string categoryName = "Test";
        await categoryService.CreateAsync(categoryName);

        // Act
        var result = await categoryService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        Assert.NotEmpty(result);
    }


    [Fact]
    public async Task GetAllAsync_ShouldtNotGetAllFromDatabase_ReturnEmptyList()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var result = await categoryService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldtUpdateEntityInDb_ReturnTrue()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        await categoryService.CreateAsync("Test");
        var foundEntity = await categoryService.GetOneAsync(x => x.CategoryName == "Test");
        foundEntity.CategoryName = "Test2";

        // Act
        var result = await categoryService.UpdateAsync(x => x.Id == foundEntity.Id, foundEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtRemoveEntityFromDb_ReturnTrue()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        await categoryService.CreateAsync("Test");

        // Act
        var result = await categoryService.DeleteAsync(x => x.CategoryName == "Test");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtNotRemoveEntityFromDb_ReturnFalse()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var result = await categoryService.DeleteAsync(x => x.CategoryName == "Test");

        // Assert
        Assert.False(result);
    }
}