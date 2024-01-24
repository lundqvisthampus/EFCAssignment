﻿using MoviesApp.Contexts;
using MoviesApp.Entities;
using System.Diagnostics;

namespace MoviesApp.Repositories;


/// <summary>
/// Repository is used for CRUD, and gets access to the Dbcontext through dependency injection.
/// </summary>
public class GenreRepository
{
    private readonly MovieDatabaseContext _context;

    public GenreRepository(MovieDatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tries to insert a genre to the database.
    /// If it fails an error will be displayed in the output, and return null.
    /// </summary>
    /// <param name="entity">An object of the GenreEntity class.</param>
    /// <returns>Returns the entity if inserted, else returns null</returns>
    public GenreEntity InsertOne(GenreEntity entity)
    {
        try
        {
            _context.Genres.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo inserting genre: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to get one genre from the database based on genreName.
    /// If it fails, error message will be displayed and return null.
    /// </summary>
    /// <param name="genreName">String with name of the genre</param>
    /// <returns>Returns object if successfull. If object not found / fails it will return null.</returns>
    public GenreEntity SelectOne(string genreName)
    {
        try
        {
            var genre = _context.Genres.FirstOrDefault(x => x.GenreName == genreName);
            if (genre != null)
                return genre;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting one genre: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to select all genres from the database.
    /// If it fails, error message will be displayed and return null. 
    /// </summary>
    /// <returns>Returns a list of genres, even if its empty. Or returns null if failed.</returns>
    public IEnumerable<GenreEntity> SelectAll()
    {
        try
        {
            var genresList = _context.Genres.ToList();
            return genresList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting all genres: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to update the object/entity in the database.
    /// </summary>
    /// <param name="entity">An object of the GenreEntity class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public bool Update(GenreEntity entity)
    {
        try
        {
            var existingEntity = _context.Genres.FirstOrDefault(x => x.Id == entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo updating the genre: {ex.Message}");
            return false;
        }
    }


    /// <summary>
    /// Tries to remove a genre from the database.
    /// </summary>
    /// <param name="entity">An object of the GenreEntity class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public bool Delete(GenreEntity entity)
    {
        try
        {
            _context.Genres.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo removing the genre: {ex.Message}");
            return false;
        }
    }
}
