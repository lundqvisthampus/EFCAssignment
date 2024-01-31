using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class MovieProviderService_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneShould_GetEntityFromDb_IfNull_InsertEntity_ReturnEntity()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);
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
        var result = await movieProviderService.InsertOne(movieDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MovieProviderEntity>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ByName_ReturnEntity()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);
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
        await movieProviderService.InsertOne(movieDto);

        // Act
        var result = movieProviderService.SelectOne(movieDto.ProviderName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ById_ReturnEntity()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);
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
        await movieProviderService.InsertOne(movieDto);

        // Act
        var result = await movieProviderService.SelectOne(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldNotGetEntityFromDb_ReturnNull()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);

        // Act
        var result = await movieProviderService.SelectOne(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAll_ShouldGetAllDirectorsFromDb_ReturnList()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);

        // Act
        var result = await movieProviderService.SelectAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MovieProviderEntity>>(result);
    }

    [Fact]
    public async Task Update_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "test" };
        await _repository.InsertOneAsync(movieProviderEntity);

        // Act
        movieProviderEntity.ProviderName = "TestUpdated";
        var result = await movieProviderService.Update(movieProviderEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Update_ShouldNotUpdateEntity_ReturnFalse()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "test" };

        // Act
        movieProviderEntity.ProviderName = "TestUpdated";
        var result = await movieProviderService.Update(movieProviderEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Delete_ShouldSelectEntityFromDb_IfExists_DeleteAndReturnTrue()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "test" };
        await _repository.InsertOneAsync(movieProviderEntity);

        // Act
        var result = await movieProviderService.Delete(movieProviderEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ShouldNotSelectEntityFromDb_ReturnFalse()
    {
        // Arrange
        var _repository = new MovieProviderRepository(_context);
        var movieProviderService = new MovieProviderService(_repository);
        var movieProviderEntity = new MovieProviderEntity { ProviderName = "test" };

        // Act
        var result = await movieProviderService.Delete(movieProviderEntity);

        // Assert
        Assert.False(result);
    }
}