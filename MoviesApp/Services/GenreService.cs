using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;

namespace MoviesApp.Services;

public class GenreService
{
    private readonly GenreRepository _repository;

    public GenreService(GenreRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Tries to insert a genre into the database if it doesnt already exist.
    /// </summary>
    /// <param name="entity">Object of GenreEntity</param>
    /// <returns>Returns the entity, or null.</returns>
    public async Task<GenreEntity> InsertOne(MovieDto dto)
    {
        try
        {
            var result = await _repository.SelectOneAsync(dto.GenreName);
            if (result == null)
            {
                var entity = new GenreEntity();
                entity.GenreName = dto.GenreName;

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
            Debug.WriteLine($"There was an issue in the GenreService when inserting an entity: {ex.Message}");
            return null!;
        }
    }

    /// <summary>
    /// Tries to select 1 genre from the database based on name.
    /// </summary>
    /// <returns>Returns either a genre, or null.</returns>
    public async Task<GenreEntity> SelectOne(string genreName)
    {
        try
        {
            var genre = await _repository.SelectOneAsync(genreName);
            if (genre != null)
                return genre;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the GenreService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    public async Task<GenreEntity> SelectOne(int Id)
    {
        try
        {
            var genre = await _repository.SelectOneAsync(Id);
            if (genre != null)
                return genre;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the GenreService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    /// <summary>
    /// Tries to select all genres from the database.
    /// </summary>
    /// <returns>Returns a list of genres, or null if an exception was cathed.</returns>
    public async Task<IEnumerable<GenreEntity>> SelectAll()
    {
        try
        {
            var listOfGenres = await _repository.SelectAllAsync();
            return listOfGenres;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the GenreService selecting all genres: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to update a genre in the database, first checks if it exists in the database.
    /// </summary>
    /// <param name="entity">Object of the type GenreEntity</param>
    /// <returns>True if updated, else null.</returns>
    public async Task<bool> Update(GenreEntity entity)
    {
        try
        {
            var genre = await SelectOne(entity.Id);
            if (genre != null)
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
            Debug.WriteLine($"There was an issue in the GenreService updating the genre: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Checks if the genre exists in the database, removes it if it exists.
    /// </summary>
    /// <param name="entity">Object of GenreEntity</param>
    /// <returns>True if object was deleted, else false.</returns>
    public async Task<bool> Delete(GenreEntity entity)
    {
        try
        {
            var result = await SelectOne(entity.GenreName);
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
            Debug.WriteLine($"There was an issue in the GenreService removing the genre: {ex.Message}");
            return false;
        }
    }
}
