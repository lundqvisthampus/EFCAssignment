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
    public bool InsertOne(MovieDto movieDto)
    {
        try
        {
            _directorService.InsertOne(movieDto);
            _genreService.InsertOne(movieDto);
            _movieProviderService.InsertOne(movieDto);
            _productionCompanyService.InsertOne(movieDto);

            MovieEntity movieEntity = new MovieEntity();
            movieEntity.Title = movieDto.Title;
            movieEntity.ReleaseYear = movieDto.ReleaseYear;

            var director = _directorService.SelectOne(movieDto.DirectorFirstName, movieDto.DirectorLastName);
            movieEntity.DirectorId = director.Id;

            var genre = _genreService.SelectOne(movieDto.GenreName);
            movieEntity.GenreId = genre.Id;

            var movieProvider = _movieProviderService.SelectOne(movieDto.ProviderName);
            if (movieProvider != null)
            {
                movieEntity.MovieProviderId = movieProvider.Id;
            }

            var productionCompany = _productionCompanyService.SelectOne(movieDto.ProductionCompanyName);
            movieEntity.ProductionCompanyId = productionCompany.Id;

            var result = _movieRepository.SelectOne(movieEntity.Title);
            if (result == null)
            {
                _movieRepository.InsertOne(movieEntity);
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

    public MovieDto SelectOne(string movieTitle)
    {
        try
        {
            var movie = _movieRepository.SelectOne(movieTitle);
            if (movie != null)
            {
                MovieDto movieDto = new MovieDto();
                var director = _directorService.SelectOne(movie.DirectorId);
                var genre = _genreService.SelectOne(movie.GenreId);
                var movieProvider = _movieProviderService.SelectOne(movie.MovieProviderId ?? 0);
                var productionCompany = _productionCompanyService.SelectOne(movie.ProductionCompanyId);

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



    public IEnumerable<MovieDto> SelectAll()
    {
        List<MovieDto> moviesList = new();

        try
        {
            var result = _movieRepository.SelectAll();

            if (result != null && result.Any())
            {
                foreach (var movie in result)
                {
                    MovieDto movieDto = new MovieDto();
                    var director = _directorService.SelectOne(movie.DirectorId);
                    var genre = _genreService.SelectOne(movie.GenreId);
                    var movieProvider = _movieProviderService.SelectOne(movie.MovieProviderId ?? 0);
                    var productionCompany = _productionCompanyService.SelectOne(movie.ProductionCompanyId);

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
    public bool Update(MovieDto movieDto, string titleOfMovieToUpdate)
    {
        var movie = _movieRepository.SelectOne(titleOfMovieToUpdate);
        if (movie != null)
        {
            try
            {
                _directorService.InsertOne(movieDto);
                var director = _directorService.SelectOne(movieDto.DirectorFirstName, movieDto.DirectorLastName);

                _genreService.InsertOne(movieDto);
                var genre = _genreService.SelectOne(movieDto.GenreName);

                _movieProviderService.InsertOne(movieDto);
                var provider = _movieProviderService.SelectOne(movieDto.ProviderName);

                _productionCompanyService.InsertOne(movieDto);
                var productionCompany = _productionCompanyService.SelectOne(movieDto.ProductionCompanyName);

                MovieEntity entity = new MovieEntity();
                entity.Id = movie.Id;
                entity.Title = movieDto.Title;
                entity.ReleaseYear = movieDto.ReleaseYear;
                entity.DirectorId = director.Id;
                entity.GenreId = genre.Id;
                entity.MovieProviderId = provider.Id;
                entity.ProductionCompanyId = productionCompany.Id;

                _movieRepository.Update(entity);
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
    public bool Delete(string movieTitle)
    {
        var movie = _movieRepository.SelectOne(movieTitle);
        try
        {
            if (movie != null)
            {
                var result = _movieRepository.Delete(movie);
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
