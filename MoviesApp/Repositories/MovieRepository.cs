using MoviesApp.Contexts;
using MoviesApp.Entities;
using System.Diagnostics;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository is used for CRUD, and gets access to the Dbcontext through dependency injection.
/// </summary>
public class MovieRepository
{

    private readonly MovieDatabaseContext _context;

    public MovieRepository(MovieDatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tries to insert a movie to the database.
    /// If it fails an error will be displayed in the output, and return null.
    /// </summary>
    /// <param name="entity">An object of the MovieEntity class.</param>
    /// <returns>Returns the entity if inserted, else returns null</returns>
    public MovieEntity InsertOne(MovieEntity entity)
    {
        try
        {
            _context.Movies.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo inserting a movie: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to get one movie from the database based on movie title.
    /// If it fails, error message will be displayed and return null.
    /// </summary>
    /// <param name="movieTitle">String with name of the movie</param>
    /// <returns>Returns object if successfull. If object not found / fails it will return null.</returns>
    public MovieEntity SelectOne(string movieTitle)
    {
        try
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Title == movieTitle);
            if (movie != null)
                return movie;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting one movie: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to select all movies from the database.
    /// If it fails, error message will be displayed and return null. 
    /// </summary>
    /// <returns>Returns a list of movies, even if its empty. Or returns null if failed.</returns>
    public IEnumerable<MovieEntity> SelectAll()
    {
        try
        {
            var movieList = _context.Movies.ToList();
            return movieList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting all movies: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to update the object/entity in the database.
    /// </summary>
    /// <param name="entity">An object of the MovieEntity class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public bool Update(MovieEntity entity)
    {
        try
        {
            var existingEntity = _context.Movies.FirstOrDefault(x => x.Id == entity.Id);
            if (existingEntity != null)
            {
                existingEntity.Title = entity.Title;
                existingEntity.ReleaseYear = entity.ReleaseYear;
                existingEntity.DirectorId = entity.DirectorId;
                existingEntity.GenreId = entity.GenreId;
                existingEntity.MovieProviderId = entity.MovieProviderId;
                existingEntity.ProductionCompanyId = entity.ProductionCompanyId;

                _context.SaveChanges();

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo updating the movie: {ex.Message}");
            return false;
        }
    }


    /// <summary>
    /// Tries to remove a movie from the database.
    /// </summary>
    /// <param name="entity">An object of the MovieEntity class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public bool Delete(MovieEntity entity)
    {
        try
        {
            _context.Movies.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo removing the movie: {ex.Message}");
            return false;
        }
    }
}
