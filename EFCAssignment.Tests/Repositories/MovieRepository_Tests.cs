using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;

namespace EFCAssignment.Tests.Repositories;

public class MovieRepository_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneAsync_ShouldAddOneEntityToDatabaseAnd_ReturnEntity()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity { Title = "Test", DirectorId = 1, GenreId = 1, MovieProviderId = 1, ProductionCompanyId = 1, ReleaseYear = 1990 };

        // Act
        var result = await movieRepository.InsertOneAsync(movieEntity);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task InsertOneAsync_ShouldNotAddOneEntityToDatabaseAnd_ReturnNull()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity();

        // Act
        var result = await movieRepository.InsertOneAsync(movieEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldGetOnemovieentityFromDatabase_ReturnEntity()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity { Title = "Test", DirectorId = 1, GenreId = 1, MovieProviderId = 1, ProductionCompanyId = 1, ReleaseYear = 1990 };
        await movieRepository.InsertOneAsync(movieEntity);

        // Act
        var result = await movieRepository.SelectOneAsync(movieEntity.Title);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldNotGetOnemovieentityFromDatabase_ReturnNull()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        string movieTitle = "Test";

        // Act
        var result = await movieRepository.SelectOneAsync(movieTitle);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAllAsync_ShouldGetAllDirectorsFromDatabase_ReturnList()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity { Title = "Test", DirectorId = 1, GenreId = 1, MovieProviderId = 1, ProductionCompanyId = 1, ReleaseYear = 1990 };
        await movieRepository.InsertOneAsync(movieEntity);

        // Act
        var result = await movieRepository.SelectAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<MovieEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity { Title = "Test", DirectorId = 1, GenreId = 1, MovieProviderId = 1, ProductionCompanyId = 1, ReleaseYear = 1990 };
        await movieRepository.InsertOneAsync(movieEntity);


        // Act
        var result = await movieRepository.UpdateAsync(movieEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_ReturnFalse()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity { Title = "Test", DirectorId = 1, GenreId = 1, MovieProviderId = 1, ProductionCompanyId = 1, ReleaseYear = 1990 };

        // Act
        var result = await movieRepository.UpdateAsync(movieEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity_ReturnTrue()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity { Title = "Test", DirectorId = 1, GenreId = 1, MovieProviderId = 1, ProductionCompanyId = 1, ReleaseYear = 1990 };
        await movieRepository.InsertOneAsync(movieEntity);

        // Act
        var result = await movieRepository.DeleteAsync(movieEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteEntity_ReturnFalse()
    {
        // Arrange
        var movieRepository = new MovieRepository(_context);
        var movieEntity = new MovieEntity { Title = "Test", DirectorId = 1, GenreId = 1, MovieProviderId = 1, ProductionCompanyId = 1, ReleaseYear = 1990 };

        // Act
        var result = await movieRepository.DeleteAsync(movieEntity);

        // Assert
        Assert.False(result);
    }
}
