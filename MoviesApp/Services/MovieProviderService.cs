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
    public async Task<MovieProviderEntity> InsertOne(MovieDto dto)
    {
        try
        {
            var result = await _repository.SelectOneAsync(dto.ProviderName);
            if (result == null)
            {
                MovieProviderEntity entity = new MovieProviderEntity();
                entity.ProviderName = dto.ProviderName;
                await _repository.InsertOneAsync(entity);
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
    public async Task<MovieProviderEntity> SelectOne(string providerName)
    {
        try
        {
            var provider = await _repository.SelectOneAsync(providerName);
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

    public async Task<MovieProviderEntity> SelectOne(int Id)
    {
        try
        {
            var provider = await _repository.SelectOneAsync(Id);
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
    public async Task<IEnumerable<MovieProviderEntity>> SelectAll()
    {
        try
        {
            var listOfProviders = await _repository.SelectAllAsync();
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
    public async Task<bool> Update(MovieProviderEntity entity)
    {
        try
        {
            var provider = await SelectOne(entity.ProviderName);
            if (provider != null)
            {
                await _repository.UpdateAsync(entity);
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
    public async Task<bool> Delete(MovieProviderEntity entity)
    {
        try
        {
            var result = await SelectOne(entity.ProviderName);
            if (result != null)
            {
                await _repository.DeleteAsync(entity);
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
