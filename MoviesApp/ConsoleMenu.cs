using MoviesApp.Dtos;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace MoviesApp;

public class ConsoleMenu
{
    private readonly MovieService _movieService;
    private readonly DirectorService _directorService;
    private readonly GenreService _genreService;
    private readonly MovieProviderService _movieProviderService;
    private readonly ProductionCompanyService _productionCompanyService;

    public ConsoleMenu(MovieService movieService, DirectorService directorService, GenreService genreService, MovieProviderService movieProviderService, ProductionCompanyService productionCompanyService)
    {
        _movieService = movieService;
        _directorService = directorService;
        _genreService = genreService;
        _movieProviderService = movieProviderService;
        _productionCompanyService = productionCompanyService;
    }
    public void AddTest()
    {
        MovieDto movieDto = new MovieDto();

        movieDto.Title = "Title3";
        movieDto.ReleaseYear = 1999;
        movieDto.DirectorFirstName = "Hampus";
        movieDto.DirectorLastName = "Lundqvist";
        movieDto.DirectorBirthDate = DateTime.Parse("1997-03-25");
        movieDto.GenreName = "Comedy";
        movieDto.ProviderName = "";
        movieDto.ProductionCompanyName = "WB";

        _movieService.InsertOne(movieDto);
    }
}
