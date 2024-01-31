using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class DirectorService_Tests
{
    private readonly MovieDatabaseContext _context =
    new(new DbContextOptionsBuilder<MovieDatabaseContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task InsertOneShould_GetEntityFromDb_IfNull_InsertEntity_ReturnEntity()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);
        var movieDto = new MovieDto 
        { 
            DirectorFirstName = "Test", DirectorLastName = "Test", 
            GenreName = "Test",
            DirectorBirthDate = DateTime.Now,
            Title = "Test",
            ReleaseYear = 1990,
            ProviderName = "Test",
            ProductionCompanyName = "Test",
        };

        // Act
        var result = await directorService.InsertOne(movieDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DirectorEntity>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ByName_ReturnEntity()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);
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
        await directorService.InsertOne(movieDto);

        // Act
        var result = directorService.SelectOne(movieDto.DirectorFirstName, movieDto.DirectorLastName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ById_ReturnEntity()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);
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
        await directorService.InsertOne(movieDto);

        // Act
        var result = await directorService.SelectOne(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldNotGetEntityFromDb_ReturnNull()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);

        // Act
        var result = await directorService.SelectOne(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAll_ShouldGetAllDirectorsFromDb_ReturnList()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);

        // Act
        var result = await directorService.SelectAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<DirectorEntity>>(result);
    }

    [Fact]
    public async Task Update_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };
        await _repository.InsertOneAsync(directorEntity);

        // Act
        directorEntity.FirstName = "TestUpdated";
        var result = await directorService.Update(directorEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Update_ShouldNotUpdateEntity_ReturnFalse()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };

        // Act
        directorEntity.FirstName = "TestUpdated";
        var result = await directorService.Update(directorEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Delete_ShouldSelectEntityFromDb_IfExists_DeleteAndReturnTrue()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };
        await _repository.InsertOneAsync(directorEntity);

        // Act
        var result = await directorService.Delete(directorEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ShouldNotSelectEntityFromDb_ReturnFalse()
    {
        // Arrange
        var _repository = new DirectorRepository(_context);
        var directorService = new DirectorService(_repository);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };

        // Act
        var result = await directorService.Delete(directorEntity);

        // Assert
        Assert.False(result);
    }
}
