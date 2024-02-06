using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;

namespace EFCAssignment.Tests.Repositories;

public class ProductReviewRepository_Tests
{
    private readonly ProductCatalogContext _context =
    new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateAsync_ShouldCreateAndAddEntityToDb_ReturnEntity()
    {
        // Arrange
        var repo = new ProductReviewRepository(_context);
        var entity = new ProductReview { Rating = 2, UserId = 1, ProductId = 1 };

        // Act
        var result = await repo.CreateAsync(entity);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneEntityFromDb_ReturnEntity()
    {
        // Arrange
        var repo = new ProductReviewRepository(_context);
        var userRepo = new UserRepository(_context);
        var user = new User { UserName = "Test", Email = "Test", Password = "Test" };
        await userRepo.CreateAsync(user);
        var productRepo = new ProductRepository(_context);
        var product = new Product { ProductName = "Test" };
        await productRepo.CreateAsync(product);
        var entity = new ProductReview { Rating = 2, UserId = 1, ProductId = 1 };
        await repo.CreateAsync(entity);

        // Act
        var result = await repo.GetOneAsync(x => x.UserId == entity.UserId && x.ProductId == entity.ProductId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductReview>(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldGetAllEntitiesFromDb_ReturnList()
    {
        // Arrange
        var repo = new ProductReviewRepository(_context);
        var entity = new ProductReview { Rating = 2, UserId = 1, ProductId = 1 };
        await repo.CreateAsync(entity);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductReview>>(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateEntityInDb_ReturnTrue()
    {
        // Arrange
        var repo = new ProductReviewRepository(_context);
        var entity = new ProductReview { Rating = 2, UserId = 1, ProductId = 1 };
        await repo.CreateAsync(entity);
        entity.Rating = 5;

        // Act
        var result = await repo.UpdateAsync(x => x.Id == entity.Id, entity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntityFromDb_ReturnTrue()
    {
        // Arrange
        var repo = new ProductReviewRepository(_context);
        var entity = new ProductReview { Rating = 2, UserId = 1, ProductId = 1 };
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
        var repo = new ProductReviewRepository(_context);
        var entity = new ProductReview { Rating = 2, UserId = 1, ProductId = 1 };

        // Act
        var result = await repo.DeleteAsync(entity);

        // Assert
        Assert.False(result);
    }
}
