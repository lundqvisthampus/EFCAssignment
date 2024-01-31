using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace EFCAssignment.Tests.Services;

public class MovieService_Tests
{
    private readonly MovieDatabaseContext _context =
        new(new DbContextOptionsBuilder<MovieDatabaseContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task InsertOneShould_GetEntityFromDb_IfNull_InsertEntity_ReturnEntity()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);

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
        var result = await _movieService.InsertOne(movieDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task SelectOne_ShouldGetEntityFromDb_ByName_ReturnEntity()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);

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
        await _movieService.InsertOne(movieDto);

        // Act
        var result = _movieService.SelectOne(movieDto.Title);

        // Assert
        Assert.NotNull(result);
    }


    [Fact]
    public async Task SelectOne_ShouldNotGetEntityFromDb_ReturnNull()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);

        // Act
        var result = await _movieService.SelectOne("test");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SelectAll_ShouldGetAllDirectorsFromDb_ReturnList()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);
        var movieDto = new MovieDto
        {
            DirectorFirstName = "Test",
            DirectorLastName = "Test",
            GenreName = "Test",
            DirectorBirthDate = DateTime.Now,
            Title = "ExistingTitle",
            ReleaseYear = 1990,
            ProviderName = "Test",
            ProductionCompanyName = "Test",
        };
        await _movieService.InsertOne(movieDto);

        // Act
        var result = await _movieService.SelectAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MovieDto>>(result);
    }

    [Fact]
    public async Task Update_ShouldUpdateExistingEntity_ReturnTrue()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);
        var movieDto = new MovieDto
        {
            DirectorFirstName = "Test",
            DirectorLastName = "Test",
            GenreName = "Test",
            DirectorBirthDate = DateTime.Now,
            Title = "ExistingTitle",
            ReleaseYear = 1990,
            ProviderName = "Test",
            ProductionCompanyName = "Test",
        };
        string titleToUpdate = "ExistingTitle";


        // Act
        await _movieService.InsertOne(movieDto);
        movieDto.Title = "UpdatedTitle";
        var result = await _movieService.Update(movieDto, titleToUpdate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Update_ShouldNotUpdateEntity_ReturnFalse()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);
        var movieDto = new MovieDto();
        string titleToUpdate = "ExistingTitle";


        // Act
        movieDto.Title = "UpdatedTitle";
        var result = await _movieService.Update(movieDto, titleToUpdate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Delete_ShouldSelectEntityFromDb_IfExists_DeleteAndReturnTrue()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);
        var movieDto = new MovieDto
        {
            DirectorFirstName = "Test",
            DirectorLastName = "Test",
            GenreName = "Test",
            DirectorBirthDate = DateTime.Now,
            Title = "ExistingTitle",
            ReleaseYear = 1990,
            ProviderName = "Test",
            ProductionCompanyName = "Test",
        };
        await _movieService.InsertOne(movieDto);

        // Act
        var result = await _movieService.Delete(movieDto.Title);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ShouldNotSelectEntityFromDb_ReturnFalse()
    {
        // Arrange
        var _directorRepository = new DirectorRepository(_context);
        var _directorService = new DirectorService(_directorRepository);
        var _genreRepository = new GenreRepository(_context);
        var _genreService = new GenreService(_genreRepository);
        var _movieProviderRepository = new MovieProviderRepository(_context);
        var _movieProviderService = new MovieProviderService(_movieProviderRepository);
        var _productionCompanyRepository = new ProductionCompanyRepository(_context);
        var _productionCompanyService = new ProductionCompanyService(_productionCompanyRepository);
        var _movieRepository = new MovieRepository(_context);
        var _movieService = new MovieService(_movieRepository, _directorService, _genreService, _movieProviderService, _productionCompanyService);
        var movieDto = new MovieDto();

        // Act
        var result = await _movieService.Delete(movieDto.Title);

        // Assert
        Assert.False(result);
    }
}