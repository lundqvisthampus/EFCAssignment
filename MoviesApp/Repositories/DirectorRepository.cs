using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using System.Diagnostics;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository is used for CRUD, and gets access to the Dbcontext through dependency injection.
/// </summary>
public class DirectorRepository
{
    private readonly MovieDatabaseContext _context;

    public DirectorRepository(MovieDatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tries to insert an director to the database.
    /// If it fails an error will be displayed in the output, and return null.
    /// </summary>
    /// <param name="entity">An object of the DirectorEntity class.</param>
    /// <returns>Returns the entity if inserted, else returns null</returns>
    public DirectorEntity InsertOne(DirectorEntity entity)
    {
        try
        {
            _context.Directors.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo inserting director: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to get one director from the database based on first & lastname, if not 
    /// If it fails, error message will be displayed and return null.
    /// </summary>
    /// <param name="firstName">String with directors firstname</param>
    /// <param name="lastName">String with directors lastname</param>
    /// <returns>Returns object if successfull. If object not found / fails it will return null.</returns>
    public DirectorEntity SelectOne(string firstName, string lastName)
    {
        try
        {
            var director = _context.Directors.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
            if (director != null)
                return director;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting one director: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to select all directors from the database.
    /// If it fails, error message will be displayed and return null. 
    /// </summary>
    /// <returns>Returns the list of directors, even if its empty. Or returns null if failed.</returns>
    public IEnumerable<DirectorEntity> SelectAll() 
    {
        try
        {
            var directorsList = _context.Directors.ToList();
            return directorsList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting all directors: {ex.Message}");
            return null!;
        }
    }
    


  /// <summary>
  /// Tries to update the object/entity in the database.
  /// </summary>
  /// <param name="entity">An object of the DirectorEntity class.</param>
  /// <returns>True if succeded, false if something fails and it throws an exception.</returns>
    public bool Update(DirectorEntity entity)
    {
        try
        {
            var existingEntity = _context.Directors.FirstOrDefault(x => x.Id == entity.Id);
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
            Debug.WriteLine($"There was an issue in the repo updating the director: {ex.Message}");
            return false;
        }
    }



    /// <summary>
    /// Tries to remove a director from the database.
    /// </summary>
    /// <param name="entity">An object of the DirectorEntity class.</param>
    /// <returns>True if succeded, false if something fails and it throws an exception.</returns>
    public bool Delete(DirectorEntity entity)
    {
        try
        {
            _context.Directors.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo removing the director: {ex.Message}");
            return false;
        }
    }
}
