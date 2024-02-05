using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;

namespace EFCAssignment.Tests.Repositories;

public class CategoryRepository_Tests
{
    private readonly ProductCatalogContext _context =
    new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateAsync_ShouldCreateAndAddEntityToDb_ReturnEntity()
    {
        // Arrange
        var repo = new CategoryRepository(_context);
        var entity = new Category { CategoryName = "Test" };

        // Act
        var result = await repo.CreateAsync(entity);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneEntityFromDb_ReturnEntity()
    {
        // Arrange
        var repo = new CategoryRepository(_context);
        var entity = new Category { CategoryName = "Test" };
        await repo.CreateAsync(entity);

        // Act
        var result = await repo.GetOneAsync(x => x.CategoryName == entity.CategoryName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Category>(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldGetAllEntitiesFromDb_ReturnList()
    {
        // Arrange
        var repo = new CategoryRepository(_context);
        var entity = new Category { CategoryName = "Test" };
        await repo.CreateAsync(entity);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Category>>(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEntityInDb_ReturnTrue()
    {
        // Arrange
        var repo = new CategoryRepository(_context);
        var entity = new Category { CategoryName = "Test" };
        await repo.CreateAsync(entity);
        entity.CategoryName = "Test2";

        // Act
        var result = await repo.UpdateAsync(x => x.Id == entity.Id, entity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntityFromDb_ReturnTrue()
    {
        // Arrange
        var repo = new CategoryRepository(_context);
        var entity = new Category { CategoryName = "Test" };
        await repo.CreateAsync(entity);

        // Act
        var result = await repo.DeleteAsync(entity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteEntityFromDb_ReturnFalse()
    {
        // Arrange
        var repo = new CategoryRepository(_context);
        var entity = new Category { CategoryName = "Test" };

        // Act
        var result = await repo.DeleteAsync(entity);

        // Assert
        Assert.False(result);
    }
}
