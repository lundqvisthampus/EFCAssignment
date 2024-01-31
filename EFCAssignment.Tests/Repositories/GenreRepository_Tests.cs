using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;

namespace EFCAssignment.Tests.Repositories;

public class GenreRepository_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneAsync_ShouldAddOneEntityToDatabaseAnd_ReturnEntity()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test"};

        // Act
        var result = await genreRepository.InsertOneAsync(genreEntity);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task InsertOneAsync_ShouldNotAddOneEntityToDatabaseAnd_ReturnNull()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity();

        // Act
        var result = await genreRepository.InsertOneAsync(genreEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldGetOnegenreEntityFromDatabase_ByName_ReturnEntity()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test" };
        await genreRepository.InsertOneAsync(genreEntity);

        // Act
        var result = await genreRepository.SelectOneAsync(genreEntity.GenreName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldGetOnegenreEntityFromDatabase_ById_ReturnEntity()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test" };
        await genreRepository.InsertOneAsync(genreEntity);

        // Act
        var result = await genreRepository.SelectOneAsync(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldNotGetOnegenreEntityFromDatabase_ReturnNull()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        string genreName = "Test";

        // Act
        var result = await genreRepository.SelectOneAsync(genreName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAllAsync_ShouldGetAllDirectorsFromDatabase_ReturnList()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test" };
        await genreRepository.InsertOneAsync(genreEntity);

        // Act
        var result = await genreRepository.SelectAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<GenreEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test" };
        await genreRepository.InsertOneAsync(genreEntity);


        // Act
        var result = await genreRepository.UpdateAsync(genreEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_ReturnFalse()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test" };

        // Act
        var result = await genreRepository.UpdateAsync(genreEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity_ReturnTrue()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test" };
        await genreRepository.InsertOneAsync(genreEntity);

        // Act
        var result = await genreRepository.DeleteAsync(genreEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteEntity_ReturnFalse()
    {
        // Arrange
        var genreRepository = new GenreRepository(_context);
        var genreEntity = new GenreEntity { GenreName = "Test" };

        // Act
        var result = await genreRepository.DeleteAsync(genreEntity);

        // Assert
        Assert.False(result);
    }
}