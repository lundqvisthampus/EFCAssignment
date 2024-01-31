using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.ComponentModel.DataAnnotations;

namespace EFCAssignment.Tests.Repositories;

public class DirectorRepository_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneAsync_ShouldAddOneEntityToDatabaseAnd_ReturnEntity()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };

        // Act
        var result = await directorRepository.InsertOneAsync(directorEntity);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task InsertOneAsync_ShouldNotAddOneEntityToDatabaseAnd_ReturnNull()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity();

        // Act
        var result = await directorRepository.InsertOneAsync(directorEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldGetOneDirectorEntityFromDatabase_ByName_ReturnEntity()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "TestFirstName", LastName = "TestLastName", BirthDate = DateTime.Now };
        await directorRepository.InsertOneAsync(directorEntity);

        // Act
        var result = await directorRepository.SelectOneAsync(directorEntity.FirstName, directorEntity.LastName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldGetOneDirectorEntityFromDatabase_ById_ReturnEntity()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "TestFirstName", LastName = "TestLastName", BirthDate = DateTime.Now };
        await directorRepository.InsertOneAsync(directorEntity);

        // Act
        var result = await directorRepository.SelectOneAsync(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOneAsync_ShouldNotGetOneDirectorEntityFromDatabase_ReturnNull()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        string firstName = "Test";
        string lastName = "Test";

        // Act
        var result = await directorRepository.SelectOneAsync(firstName, lastName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAllAsync_ShouldGetAllDirectorsFromDatabase_ReturnList()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };
        await directorRepository.InsertOneAsync(directorEntity);

        // Act
        var result = await directorRepository.SelectAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<DirectorEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };
        await directorRepository.InsertOneAsync(directorEntity);


        // Act
        var result = await directorRepository.UpdateAsync(directorEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_ReturnFalse()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };

        // Act
        var result = await directorRepository.UpdateAsync(directorEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteEntity_ReturnTrue()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };
        await directorRepository.InsertOneAsync(directorEntity);

        // Act
        var result = await directorRepository.DeleteAsync(directorEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteEntity_ReturnFalse()
    {
        // Arrange
        var directorRepository = new DirectorRepository(_context);
        var directorEntity = new DirectorEntity { FirstName = "Test", LastName = "Test", BirthDate = DateTime.Now };

        // Act
        var result = await directorRepository.DeleteAsync(directorEntity);

        // Assert
        Assert.False(result);
    }
}
