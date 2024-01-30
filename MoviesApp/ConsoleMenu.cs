using Microsoft.IdentityModel.Tokens;
using MoviesApp.Dtos;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace MoviesApp;

public class ConsoleMenu
{
    private readonly MovieService _movieService;


    public ConsoleMenu(MovieService movieService)
    {
        _movieService = movieService;
    }
    
    public async Task ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("------  MOVIE DATABASE MENU  ------");
            Console.WriteLine("Choose one of the following options.");
            Console.WriteLine("\n1 - Add a movie.");
            Console.WriteLine("2 - Get all movies.");
            Console.WriteLine("3 - Get one movie.");
            Console.WriteLine("4 - Update a movie.");
            Console.WriteLine("5 - Delete a movie.");

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        MovieDto movie = new MovieDto();
                        Console.WriteLine("-- Enter movie info --");
                        Console.Write("Movie title: ");
                        string title = Console.ReadLine()!;

                        if (!string.IsNullOrEmpty(title))
                        {
                            movie.Title = title;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid movie title.");
                        }

                        Console.Write("Movie release year: ");
                        if (int.TryParse(Console.ReadLine(), out int releaseYear))
                        {
                            movie.ReleaseYear = releaseYear;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid release year.");
                        }

                        Console.Clear();
                        Console.WriteLine("-- Enter director info --");
                        Console.Write("Director's firstname: ");
                        string firstName = Console.ReadLine()!;

                        if (!string.IsNullOrEmpty(firstName))
                        {
                            movie.DirectorFirstName = firstName;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid name.");
                        }

                        Console.Write("Director's lastname: ");
                        string lastName = Console.ReadLine()!;

                        if (!string.IsNullOrEmpty(lastName))
                        {
                            movie.DirectorLastName = lastName;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid name.");
                        }

                        Console.Write("Director's birthdate, example 1993-04-25: ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
                        {
                            movie.DirectorBirthDate = birthDate;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a date name. For example 1993-04-25");
                        }

                        Console.Clear();
                        Console.WriteLine("-- Enter provider info --");
                        Console.Write("Name of provider, for example Netflix: ");
                        string provider = Console.ReadLine()!;

                        if (!string.IsNullOrEmpty(provider))
                        {
                            movie.ProviderName = provider;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid provider.");
                        }

                        Console.Clear();
                        Console.WriteLine("-- Enter production company info --");
                        Console.Write("Name of company, for example Warner Bros: ");
                        string company = Console.ReadLine()!;

                        if (!string.IsNullOrEmpty(company))
                        {
                            movie.ProductionCompanyName = company;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid company.");
                        }

                        Console.Clear();
                        Console.WriteLine("-- Enter genre --");
                        Console.Write("Genre name: ");
                        string genre = Console.ReadLine()!;

                        if (!string.IsNullOrEmpty(genre))
                        {
                            movie.GenreName = genre;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a genre.");
                        }

                        var result = await _movieService.InsertOne(movie);

                        if (result)
                        {
                            Console.Clear();
                            Console.WriteLine("Movie added to database!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Something went wrong, movie not added.");
                            Console.ReadKey();
                        }
                        break;

                    case 2:

                        var moviesList = await _movieService.SelectAll();

                        Console.Clear();
                        Console.WriteLine("-- All movies currently in the list --");
                        Console.WriteLine("----------------------------------------");
                        foreach (var item  in moviesList)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine($"Title: {item.Title}");
                            Console.WriteLine($"Release year: {item.ReleaseYear}");
                            Console.WriteLine($"\nGenre: {item.GenreName}");
                            Console.WriteLine($"\nDirector's name: {item.DirectorFirstName} {item.DirectorLastName}");
                            Console.WriteLine($"Director's birthdate: {item.DirectorBirthDate}");
                            Console.WriteLine($"\nProduction company: {item.ProductionCompanyName}");
                            Console.WriteLine($"\nAvailabe for watching: {item.ProviderName}");
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("");
                        }
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        Console.Write("Title of movie you want to find: ");
                        string option = Console.ReadLine()!;
                        if (!string.IsNullOrEmpty(option))
                        {
                            var findMovie = await _movieService.SelectOne(option);
                            Console.Clear();
                            if (findMovie != null)
                            {
                                Console.WriteLine($"Title: {findMovie.Title}");
                                Console.WriteLine($"Release year: {findMovie.ReleaseYear}");
                                Console.WriteLine($"\nGenre: {findMovie.GenreName}");
                                Console.WriteLine($"\nDirector's name: {findMovie.DirectorFirstName} {findMovie.DirectorLastName}");
                                Console.WriteLine($"Director's birthdate: {findMovie.DirectorBirthDate}");
                                Console.WriteLine($"\nProduction company: {findMovie.ProductionCompanyName}");
                                Console.WriteLine($"\nAvailabe for watching: {findMovie.ProviderName}");
                            }
                            else
                            {
                                Console.WriteLine("Movie not found or something went wrong.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong, please enter a valid title");
                        }
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.Clear();
                        Console.Write("Title of movie you want to update: ");
                        string updateMovie = Console.ReadLine()!;
                        if (!string.IsNullOrEmpty(updateMovie))
                        {
                            MovieDto movieDto = new MovieDto();
                            var movieToUpdate = await _movieService.SelectOne(updateMovie);

                            Console.Clear();
                            Console.WriteLine("-- Selected movie --");
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine($"\nTitle: {movieToUpdate.Title}");
                            Console.WriteLine($"Release year: {movieToUpdate.ReleaseYear}");
                            Console.WriteLine($"\nGenre: {movieToUpdate.GenreName}");
                            Console.WriteLine($"\nDirector's name: {movieToUpdate.DirectorFirstName} {movieToUpdate.DirectorLastName}");
                            Console.WriteLine($"Director's birthdate: {movieToUpdate.DirectorBirthDate}");
                            Console.WriteLine($"\nProduction company: {movieToUpdate.ProductionCompanyName}");
                            Console.WriteLine($"\nAvailabe for watching: {movieToUpdate.ProviderName}");
                            Console.WriteLine("---------------------------------------");

                            Console.Write("\nNew title: ");
                            movieDto.Title = Console.ReadLine()!;

                            Console.Write("New release year: ");
                            int.TryParse(Console.ReadLine(), out int newReleaseYear);
                            movieDto.ReleaseYear = newReleaseYear;

                            Console.Write("New genre: ");
                            movieDto.GenreName = Console.ReadLine()!;

                            Console.Write("New director firstname: ");
                            movieDto.DirectorFirstName = Console.ReadLine()!;

                            Console.Write("New director lastname: ");
                            movieDto.DirectorLastName = Console.ReadLine()!;

                            Console.Write("New director birthdate: ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime newBirthDate))
                            {
                                movieDto.DirectorBirthDate = newBirthDate;
                            }

                            Console.Write("New production company: ");
                            movieDto.ProductionCompanyName = Console.ReadLine()!;

                            Console.Write("New movie provider (Ex. Netflix): ");
                            movieDto.ProviderName = Console.ReadLine()!;

                            var isUpdated = await _movieService.Update(movieDto, updateMovie);
                            Console.Clear();
                            if (isUpdated)
                            {
                                Console.WriteLine("Movie was updated!");
                            }
                            else
                            {
                                Console.WriteLine("Something went wrong, movie not updated.");
                            }
                        }
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.Clear();
                        Console.Write("Movie title to delete: ");
                        string movieToDelete = Console.ReadLine()!;
                        if (movieToDelete != null)
                        {
                            var wasDeleted = await _movieService.Delete(movieToDelete);
                            Console.Clear();
                            if (wasDeleted)
                            {
                                Console.WriteLine("Movie was deleted.");
                            }
                            else 
                            {
                                Console.WriteLine("Something went wrong, movie was not deleted");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid title");
                        }
                        Console.ReadKey();
                        break;
                }   
            }
        }
    }
}
