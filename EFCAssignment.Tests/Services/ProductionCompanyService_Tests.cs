using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class ProductionCompanyService_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneShould_GetEntityFromDb_IfNull_InsertEntity_ReturnEntity()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);
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
        var result = await productionCompanyService.InsertOne(movieDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductionCompanyEntity>(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ByName_ReturnEntity()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);
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
        await productionCompanyService.InsertOne(movieDto);

        // Act
        var result = productionCompanyService.SelectOne(movieDto.ProductionCompanyName);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ById_ReturnEntity()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);
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
        await productionCompanyService.InsertOne(movieDto);

        // Act
        var result = await productionCompanyService.SelectOne(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SelectOne_ShouldNotGetEntityFromDb_ReturnNull()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository); ;

        // Act
        var result = await productionCompanyService.SelectOne(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAll_ShouldGetAllDirectorsFromDb_ReturnList()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);

        // Act
        var result = await productionCompanyService.SelectAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ProductionCompanyEntity>>(result);
    }

    [Fact]
    public async Task Update_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "test" };
        await _repository.InsertOneAsync(productionCompanyEntity);

        // Act
        productionCompanyEntity.CompanyName = "TestUpdated";
        var result = await productionCompanyService.Update(productionCompanyEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Update_ShouldNotUpdateEntity_ReturnFalse()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "test" };

        // Act
        productionCompanyEntity.CompanyName = "TestUpdated";
        var result = await productionCompanyService.Update(productionCompanyEntity);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Delete_ShouldSelectEntityFromDb_IfExists_DeleteAndReturnTrue()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "test" };
        await _repository.InsertOneAsync(productionCompanyEntity);

        // Act
        var result = await productionCompanyService.Delete(productionCompanyEntity);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ShouldNotSelectEntityFromDb_ReturnFalse()
    {
        // Arrange
        var _repository = new ProductionCompanyRepository(_context);
        var productionCompanyService = new ProductionCompanyService(_repository);
        var productionCompanyEntity = new ProductionCompanyEntity { CompanyName = "test" };

        // Act
        var result = await productionCompanyService.Delete(productionCompanyEntity);

        // Assert
        Assert.False(result);
    }
}