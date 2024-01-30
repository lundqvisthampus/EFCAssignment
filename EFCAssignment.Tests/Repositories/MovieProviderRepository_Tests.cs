using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;

namespace EFCAssignment.Tests.Repositories;

public class MovieProviderRepository_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneAsync_ShouldAddOneEntityToDatabaseAnd_ReturnEntity()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "Test" };

        // Act
        var result = await movieProviderRepository.InsertOneAsync(movieProviderEntity);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task InsertOneAsync_ShouldNotAddOneEntityToDatabaseAnd_ReturnNull()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity();

        // Act
        var result = await movieProviderRepository.InsertOneAsync(movieProviderEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldGetOneMovieProviderEntityFromDatabase_ReturnEntity()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "Test" };
        await movieProviderRepository.InsertOneAsync(movieProviderEntity);

        // Act
        var result = await movieProviderRepository.SelectOneAsync(movieProviderEntity.ProviderName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldNotGetOneMovieProviderEntityFromDatabase_ReturnNull()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        string genreName = "Test";

        // Act
        var result = await movieProviderRepository.SelectOneAsync(genreName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAllAsync_ShouldGetAllDirectorsFromDatabase_ReturnList()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "Test" };
        await movieProviderRepository.InsertOneAsync(movieProviderEntity);

        // Act
        var result = await movieProviderRepository.SelectAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<MovieProviderEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "Test" };
        await movieProviderRepository.InsertOneAsync(movieProviderEntity);


        // Act
        var result = await movieProviderRepository.UpdateAsync(movieProviderEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_ReturnFalse()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "Test" };

        // Act
        var result = await movieProviderRepository.UpdateAsync(movieProviderEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity_ReturnTrue()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "Test" };
        await movieProviderRepository.InsertOneAsync(movieProviderEntity);

        // Act
        var result = await movieProviderRepository.DeleteAsync(movieProviderEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteEntity_ReturnFalse()
    {
        // Arrange
        var movieProviderRepository = new MovieProviderRepository(_context);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "Test" };

        // Act
        var result = await movieProviderRepository.DeleteAsync(movieProviderEntity);

        // Assert
        Assert.False(result);
    }
}