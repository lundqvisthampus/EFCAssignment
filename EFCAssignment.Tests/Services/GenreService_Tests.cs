using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class GenreService_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneShould_GetEntityFromDb_IfNull_InsertEntity_ReturnEntity()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);
        var movieDto = new MovieDto
        {
            DirectorFirstName = "Test",
            DirectorLastName = "Test",
            GenreName = "Test",
            DirectorBirthDate = DateTime.Now,
            Title = "Test",
            ReleaseYear = 1990,
            ProviderName = "Test",
            ProductionCompanyName = "Test",
        };

        // Act
        var result = await genreService.InsertOne(movieDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<GenreEntity>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ByName_ReturnEntity()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);
        var movieDto = new MovieDto
        {
            DirectorFirstName = "Test",
            DirectorLastName = "Test",
            GenreName = "Test",
            DirectorBirthDate = DateTime.Now,
            Title = "Test",
            ReleaseYear = 1990,
            ProviderName = "Test",
            ProductionCompanyName = "Test",
        };
        await genreService.InsertOne(movieDto);

        // Act
        var result = genreService.SelectOne(movieDto.GenreName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ById_ReturnEntity()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);
        var movieDto = new MovieDto
        {
            DirectorFirstName = "Test",
            DirectorLastName = "Test",
            GenreName = "Test",
            DirectorBirthDate = DateTime.Now,
            Title = "Test",
            ReleaseYear = 1990,
            ProviderName = "Test",
            ProductionCompanyName = "Test",
        };
        await genreService.InsertOne(movieDto);

        // Act
        var result = await genreService.SelectOne(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldNotGetEntityFromDb_ReturnNull()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);

        // Act
        var result = await genreService.SelectOne(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAll_ShouldGetAllDirectorsFromDb_ReturnList()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);

        // Act
        var result = await genreService.SelectAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<GenreEntity>>(result);
    }

    [Fact]
    public async Task Update_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);
        var genreEntity = new GenreEntity { GenreName = "test" };
        await _repository.InsertOneAsync(genreEntity);

        // Act
        genreEntity.GenreName = "TestUpdated";
        var result = await genreService.Update(genreEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Update_ShouldNotUpdateEntity_ReturnFalse()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);
        var genreEntity = new GenreEntity { GenreName = "test" };

        // Act
        genreEntity.GenreName = "TestUpdated";
        var result = await genreService.Update(genreEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Delete_ShouldSelectEntityFromDb_IfExists_DeleteAndReturnTrue()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);
        var genreEntity = new GenreEntity { GenreName = "test" };
        await _repository.InsertOneAsync(genreEntity);

        // Act
        var result = await genreService.Delete(genreEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ShouldNotSelectEntityFromDb_ReturnFalse()
    {
        // Arrange
        var _repository = new GenreRepository(_context);
        var genreService = new GenreService(_repository);
        var genreEntity = new GenreEntity { GenreName = "test" };

        // Act
        var result = await genreService.Delete(genreEntity);

        // Assert
        Assert.False(result);
    }
}
