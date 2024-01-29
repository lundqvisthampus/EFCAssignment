using Microsoft.Identity.Client;
using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;

namespace MoviesApp.Services;

public class MovieService
{
    private readonly MovieRepository _movieRepository;
    private readonly DirectorService _directorService;
    private readonly GenreService _genreService;
    private readonly MovieProviderService _movieProviderService;
    private readonly ProductionCompanyService _productionCompanyService;

    public MovieService(MovieRepository movieRepository, DirectorService directorService, GenreService genreService, MovieProviderService movieProviderService, ProductionCompanyService productionCompanyService)
    {
        _movieRepository = movieRepository;
        _directorService = directorService;
        _genreService = genreService;
        _movieProviderService = movieProviderService;
        _productionCompanyService = productionCompanyService;
    }

    // CREATE
    public async Task<bool> InsertOne(MovieDto movieDto)
    {
        try
        {
            await _directorService.InsertOne(movieDto);
            await _genreService.InsertOne(movieDto);
            await _movieProviderService.InsertOne(movieDto);
            await _productionCompanyService.InsertOne(movieDto);

            MovieEntity movieEntity = new MovieEntity();
            movieEntity.Title = movieDto.Title;
            movieEntity.ReleaseYear = movieDto.ReleaseYear;

            var director = await _directorService.SelectOne(movieDto.DirectorFirstName, movieDto.DirectorLastName);
            movieEntity.DirectorId = director.Id;

            var genre = await _genreService.SelectOne(movieDto.GenreName);
            movieEntity.GenreId = genre.Id;

            var movieProvider = await _movieProviderService.SelectOne(movieDto.ProviderName);
            if (movieProvider != null)
            {
                movieEntity.MovieProviderId = movieProvider.Id;
            }

            var productionCompany = await _productionCompanyService.SelectOne(movieDto.ProductionCompanyName);
            movieEntity.ProductionCompanyId = productionCompany.Id;

            var result = await _movieRepository.SelectOneAsync(movieEntity.Title);
            if (result == null)
            {
                await _movieRepository.InsertOneAsync(movieEntity);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error inserting movie in MovieSerice: {ex.Message}");
            return false;
        }
    }

    // READ

    public async Task<MovieDto> SelectOne(string movieTitle)
    {
        try
        {
            var movie = await _movieRepository.SelectOneAsync(movieTitle);
            if (movie != null)
            {
                var director = await _directorService.SelectOne(movie.DirectorId);
                var genre = await _genreService.SelectOne(movie.GenreId);
                var movieProvider = await _movieProviderService.SelectOne(movie.MovieProviderId ?? 0);
                var productionCompany = await _productionCompanyService.SelectOne(movie.ProductionCompanyId);

                MovieDto movieDto = new MovieDto();
                movieDto.Title = movie.Title;
                movieDto.ReleaseYear = movie.ReleaseYear;
                movieDto.DirectorFirstName = director.FirstName;
                movieDto.DirectorLastName = director.LastName;
                movieDto.DirectorBirthDate = director.BirthDate;
                movieDto.GenreName = genre.GenreName;
                movieDto.ProviderName = movieProvider.ProviderName;
                movieDto.ProductionCompanyName = productionCompany.CompanyName;
                return movieDto;
            }
            else
            {
                return null!;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error selecing all movies in movieservice: {ex.Message}");
            return null!;
        }
    }



    public async Task<IEnumerable<MovieDto>> SelectAll()
    {
        List<MovieDto> moviesList = new();

        try
        {
            var result = await _movieRepository.SelectAllAsync();

            if (result != null && result.Any())
            {
                foreach (var movie in result)
                {
                    MovieDto movieDto = new MovieDto();
                    var director = await _directorService.SelectOne(movie.DirectorId);
                    var genre = await _genreService.SelectOne(movie.GenreId);
                    var movieProvider = await _movieProviderService.SelectOne(movie.MovieProviderId ?? 0);
                    var productionCompany = await _productionCompanyService.SelectOne(movie.ProductionCompanyId);

                    movieDto.Title = movie.Title;
                    movieDto.ReleaseYear = movie.ReleaseYear;
                    movieDto.DirectorFirstName = director.FirstName;
                    movieDto.DirectorLastName = director.LastName;
                    movieDto.DirectorBirthDate = director.BirthDate;
                    movieDto.GenreName = genre.GenreName;
                    movieDto.ProviderName = movieProvider.ProviderName;
                    movieDto.ProductionCompanyName = productionCompany.CompanyName;

                    moviesList.Add(movieDto);
                }
                return moviesList;
            }
            else 
            {
                return Enumerable.Empty<MovieDto>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error selecing all movies in movieservice: {ex.Message}");
            return Enumerable.Empty<MovieDto>();
        }
    }

    // UPDATE
    public async Task<bool> Update(MovieDto movieDto, string titleOfMovieToUpdate)
    {
        var movie = await _movieRepository.SelectOneAsync(titleOfMovieToUpdate);
        if (movie != null)
        {
            try
            {
                await _directorService.InsertOne(movieDto);
                var director = await _directorService.SelectOne(movieDto.DirectorFirstName, movieDto.DirectorLastName);

                await _genreService.InsertOne(movieDto);
                var genre = await _genreService.SelectOne(movieDto.GenreName);

                await _movieProviderService.InsertOne(movieDto);
                var provider = await _movieProviderService.SelectOne(movieDto.ProviderName);

                await _productionCompanyService.InsertOne(movieDto);
                var productionCompany = await _productionCompanyService.SelectOne(movieDto.ProductionCompanyName);

                MovieEntity entity = new MovieEntity();
                entity.Id = movie.Id;
                entity.Title = movieDto.Title;
                entity.ReleaseYear = movieDto.ReleaseYear;
                entity.DirectorId = director.Id;
                entity.GenreId = genre.Id;
                entity.MovieProviderId = provider.Id;
                entity.ProductionCompanyId = productionCompany.Id;

                await _movieRepository.UpdateAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating movie in movieservice: {ex.Message}");
                return false;
            }
        }
        return false;
    }

    // DELETE
    public async Task<bool> Delete(string movieTitle)
    {
        var movie = await _movieRepository.SelectOneAsync(movieTitle);
        try
        {
            if (movie != null)
            {
                var result = await _movieRepository.DeleteAsync(movie);
                return result;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting a movie in movieservice: {ex.Message}");
            return false;
        }

    }
}
