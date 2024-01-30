using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;

namespace EFCAssignment.Tests.Repositories;

public class ProductionCompanyRepository_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneAsync_ShouldAddOneEntityToDatabaseAnd_ReturnEntity()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "Test" };

        // Act
        var result = await productionCompanyRepository.InsertOneAsync(productionCompanyEntity);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task InsertOneAsync_ShouldNotAddOneEntityToDatabaseAnd_ReturnNull()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity();

        // Act
        var result = await productionCompanyRepository.InsertOneAsync(productionCompanyEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldGetOneProductionCompanyEntityFromDatabase_ReturnEntity()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "Test" };
        await productionCompanyRepository.InsertOneAsync(productionCompanyEntity);

        // Act
        var result = await productionCompanyRepository.SelectOneAsync(productionCompanyEntity.CompanyName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldNotGetOneProductionCompanyEntityFromDatabase_ReturnNull()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        string companyName = "Test";

        // Act
        var result = await productionCompanyRepository.SelectOneAsync(companyName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAllAsync_ShouldGetAllDirectorsFromDatabase_ReturnList()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "Test" };
        await productionCompanyRepository.InsertOneAsync(productionCompanyEntity);

        // Act
        var result = await productionCompanyRepository.SelectAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductionCompanyEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "Test" };
        await productionCompanyRepository.InsertOneAsync(productionCompanyEntity);


        // Act
        var result = await productionCompanyRepository.UpdateAsync(productionCompanyEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_ReturnFalse()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "Test" };

        // Act
        var result = await productionCompanyRepository.UpdateAsync(productionCompanyEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity_ReturnTrue()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "Test" };
        await productionCompanyRepository.InsertOneAsync(productionCompanyEntity);

        // Act
        var result = await productionCompanyRepository.DeleteAsync(productionCompanyEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteEntity_ReturnFalse()
    {
        // Arrange
        var productionCompanyRepository = new ProductionCompanyRepository(_context);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "Test" };

        // Act
        var result = await productionCompanyRepository.DeleteAsync(productionCompanyEntity);

        // Assert
        Assert.False(result);
    }
}