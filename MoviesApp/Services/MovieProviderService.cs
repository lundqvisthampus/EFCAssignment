using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;

namespace MoviesApp.Services;

public class MovieProviderService
{
    private readonly MovieProviderRepository _repository;

    public MovieProviderService(MovieProviderRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Tries to insert a provider into the database if it doesnt already exist.
    /// </summary>
    /// <param name="entity">Object of MovieProviderEntity</param>
    /// <returns>Returns the entity, or null.</returns>
    public MovieProviderEntity InsertOne(MovieDto dto)
    {
        try
        {
            var result = _repository.SelectOne(dto.ProviderName);
            if (result == null)
            {
                MovieProviderEntity entity = new MovieProviderEntity();
                entity.ProviderName = dto.ProviderName;
                _repository.InsertOne(entity);
                return entity;
            }
            else
            {
                return null!;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the MovieProviderService when inserting an entity: {ex.Message}");
            return null!;
        }
    }

    /// <summary>
    /// Tries to select 1 provider from the database based on name.
    /// </summary>
    /// <returns>Returns either a provider, or null.</returns>
    public MovieProviderEntity SelectOne(string providerName)
    {
        try
        {
            var provider = _repository.SelectOne(providerName);
            if (provider != null)
                return provider;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the MovieProviderService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    public MovieProviderEntity SelectOne(int Id)
    {
        try
        {
            var provider = _repository.SelectOne(Id);
            if (provider != null)
                return provider;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the MovieProviderService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    /// <summary>
    /// Tries to select all providers from the database.
    /// </summary>
    /// <returns>Returns a list of providers, or null if an exception was cathed.</returns>
    public IEnumerable<MovieProviderEntity> SelectAll()
    {
        try
        {
            var listOfProviders = _repository.SelectAll();
            return listOfProviders;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the MovieProviderService selecting all providers: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to update a provider in the database, first checks if it exists in the database.
    /// </summary>
    /// <param name="entity">Object of the type MovieProviderEntity</param>
    /// <returns>True if updated, else null.</returns>
    public bool Update(MovieProviderEntity entity)
    {
        try
        {
            var provider = SelectOne(entity.ProviderName);
            if (provider != null)
            {
                _repository.Update(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the MovieProviderService updating the provider: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Checks if the provider exists in the database, removes it if it exists.
    /// </summary>
    /// <param name="entity">Object of MovieProviderEntity</param>
    /// <returns>True if object was deleted, else false.</returns>
    public bool Delete(MovieProviderEntity entity)
    {
        try
        {
            var result = SelectOne(entity.ProviderName);
            if (result != null)
            {
                _repository.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the MovieProviderService removing the provider: {ex.Message}");
            return false;
        }
    }
}
