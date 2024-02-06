using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class UserService_Tests
{
    private readonly ProductCatalogContext _context =
    new(new DbContextOptionsBuilder<ProductCatalogContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public async Task CreateAsync_ShouldAddEntityToDbIfNotExist_ReturnEntity()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var userDto = new UserDto
        {
            UserName = "Test",
            Email = "Test@Domain.com",
            Password = "BytMig123!"
        };

        // Act
        var result = await userService.CreateAsync(userDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<User>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGetOneFromDatabase_ReturnEntity()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var userDto = new UserDto
        {
            UserName = "Test",
            Email = "Test@Domain.com",
            Password = "BytMig123!"
        };
        await userService.CreateAsync(userDto);

        // Act
        var result = await userService.GetOneAsync(x => x.UserName == userDto.UserName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<User>(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldNotGetOneFromDatabase_ReturnNull()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        string userName = "Test";

        // Act
        var result = await userService.GetOneAsync(x => x.UserName == userName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldtGetAllFromDatabase_ReturnList()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var userDto = new UserDto
        {
            UserName = "Test",
            Email = "Test@Domain.com",
            Password = "BytMig123!"
        };
        await userService.CreateAsync(userDto);

        // Act
        var result = await userService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<User>>(result);
        Assert.NotEmpty(result);
    }


    [Fact]
    public async Task GetAllAsync_ShouldtNotGetAllFromDatabase_ReturnEmptyList()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);

        // Act
        var result = await userService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<User>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldtUpdateEntityInDb_ReturnTrue()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var userDto = new UserDto
        {
            UserName = "Test",
            Email = "Test@Domain.com",
            Password = "BytMig123!"
        };
        await userService.CreateAsync(userDto);
        var foundEntity = await userService.GetOneAsync(x => x.UserName == userDto.UserName);
        foundEntity.UserName = "Test2";

        // Act
        var result = await userService.UpdateAsync(x => x.Id == foundEntity.Id, foundEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtRemoveEntityFromDb_ReturnTrue()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var userDto = new UserDto
        {
            UserName = "Test",
            Email = "Test@Domain.com",
            Password = "BytMig123!"
        };
        await userService.CreateAsync(userDto);

        // Act
        var result = await userService.DeleteAsync(x => x.UserName == "Test");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldtNotRemoveEntityFromDb_ReturnFalse()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);

        // Act
        var result = await userService.DeleteAsync(x => x.UserName == "Test");

        // Assert
        Assert.False(result);
    }
}