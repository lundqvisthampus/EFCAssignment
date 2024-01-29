using Microsoft.EntityFrameworkCore;
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
    public async Task<MovieEntity> InsertOneAsync(MovieEntity entity)
    {
        try
        {
            _context.Movies.Add(entity);
            await _context.SaveChangesAsync();
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
    public async Task<MovieEntity> SelectOneAsync(string movieTitle)
    {
        try
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Title == movieTitle);
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
    public async Task<IEnumerable<MovieEntity>> SelectAllAsync()
    {
        try
        {
            var movieList = await _context.Movies.ToListAsync();
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
    public async Task<bool> UpdateAsync(MovieEntity entity)
    {
        try
        {
            var existingEntity = await _context.Movies.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (existingEntity != null)
            {
                existingEntity.Title = entity.Title;
                existingEntity.ReleaseYear = entity.ReleaseYear;
                existingEntity.DirectorId = entity.DirectorId;
                existingEntity.GenreId = entity.GenreId;
                existingEntity.MovieProviderId = entity.MovieProviderId;
                existingEntity.ProductionCompanyId = entity.ProductionCompanyId;

                await _context.SaveChangesAsync();

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
    public async Task<bool> DeleteAsync(MovieEntity entity)
    {
        try
        {
            _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo removing the movie: {ex.Message}");
            return false;
        }
    }
}
