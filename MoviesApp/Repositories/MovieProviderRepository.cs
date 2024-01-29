using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using System.Diagnostics;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository is used for CRUD, and gets access to the Dbcontext through dependency injection.
/// </summary>
public class MovieProviderRepository
{
    private readonly MovieDatabaseContext _context;

    public MovieProviderRepository(MovieDatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tries to insert a MovieProvider to the database.
    /// If it fails an error will be displayed in the output, and return null.
    /// </summary>
    /// <param name="entity">An object of the MovieProvider class.</param>
    /// <returns>Returns the entity if inserted, else returns null</returns>
    public async Task<MovieProviderEntity> InsertOneAsync(MovieProviderEntity entity)
    {
        try
        {
            _context.MovieProviders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo inserting movie provider: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to get one movie provider from the database based on provider name.
    /// If it fails, error message will be displayed and return null.
    /// </summary>
    /// <param name="providerName">String with name of the provider</param>
    /// <returns>Returns object if successfull. If object not found / fails it will return null.</returns>
    public async Task<MovieProviderEntity> SelectOneAsync(string providerName)
    {
        try
        {
            var provider = await _context.MovieProviders.FirstOrDefaultAsync(x => x.ProviderName == providerName);
            if (provider != null)
                return provider;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting one movie provider: {ex.Message}");
            return null!;
        }
    }

    public async Task<MovieProviderEntity> SelectOneAsync(int Id)
    {
        try
        {
            var provider = await _context.MovieProviders.FirstOrDefaultAsync(x => x.Id == Id);
            if (provider != null)
                return provider;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting one movie provider: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to select all movie providers from the database.
    /// If it fails, error message will be displayed and return null. 
    /// </summary>
    /// <returns>Returns a list of movie providers, even if its empty. Or returns null if failed.</returns>
    public async Task<IEnumerable<MovieProviderEntity>> SelectAllAsync()
    {
        try
        {
            var providerList = await _context.MovieProviders.ToListAsync();
            return providerList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting all movie providers: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to update the object/entity in the database.
    /// </summary>
    /// <param name="entity">An object of the MovieProvider class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public async Task<bool> UpdateAsync(MovieProviderEntity entity)
    {
        try
        {
            var existingEntity = await _context.MovieProviders.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo updating the movie provider: {ex.Message}");
            return false;
        }
    }


    /// <summary>
    /// Tries to remove a movie provider from the database.
    /// </summary>
    /// <param name="entity">An object of the MovieProviderEntity class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public async Task<bool> DeleteAsync(MovieProviderEntity entity)
    {
        try
        {
            _context.MovieProviders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo removing the movie provider: {ex.Message}");
            return false;
        }
    }
}
