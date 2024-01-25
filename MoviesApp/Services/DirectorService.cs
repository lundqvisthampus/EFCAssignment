using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;

namespace MoviesApp.Services;

public class DirectorService
{
    private readonly DirectorRepository _repository;

    public DirectorService(DirectorRepository repository)
    {
        _repository = repository;
    }

   /// <summary>
   /// Tries to insert a director into the database if it doesnt already exist.
   /// </summary>
   /// <param name="entity">Object of directorentity</param>
   /// <returns>Returns the entity, or null.</returns>
    public DirectorEntity InsertOne(MovieDto dto)
    {
        try
        {
            var result = _repository.SelectOne(dto.DirectorFirstName, dto.DirectorLastName);
            if (result == null)
            {
                var entity = new DirectorEntity();
                entity.FirstName = dto.DirectorFirstName;
                entity.LastName = dto.DirectorLastName;
                entity.BirthDate = dto.DirectorBirthDate;

                _repository.InsertOne(entity);
                return entity;
            }
            else
            {
                return result!;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the DirectorService when inserting an entity: {ex.Message}");
            return null!;
        }
    }

 /// <summary>
 /// Tries to select 1 director from the database based on name.
 /// </summary>
 /// <param name="firstName">Directors firstname</param>
 /// <param name="lastName">Directors lastname</param>
 /// <returns>Returns either a director, or null.</returns>
    public DirectorEntity SelectOne(string firstName, string lastName)
    {
        try
        {
            var director = _repository.SelectOne(firstName, lastName);
            if (director != null)
                return director;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the DirectorService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    public DirectorEntity SelectOne(int Id)
    {
        try
        {
            var director = _repository.SelectOne(Id);
            if (director != null)
                return director;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the DirectorService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    /// <summary>
    /// Tries to select all directors from the database.
    /// </summary>
    /// <returns>Returns a list of directors, or null if an exception was cathed.</returns>
    public IEnumerable<DirectorEntity> SelectAll() 
    {
        try
        {
            var listOfDirectors = _repository.SelectAll();
            return listOfDirectors;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the DirectorService selecting all directors: {ex.Message}");
            return null!;
        }
    }


  /// <summary>
  /// Tries to update a director in the database, first checks if it exists in the database.
  /// </summary>
  /// <param name="entity">Object of the type DirectorEntity</param>
  /// <returns>True if updated, else null.</returns>
    public bool Update(DirectorEntity entity)
    {
        try
        {
            var director = SelectOne(entity.FirstName, entity.LastName);
            if (director != null)
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
            Debug.WriteLine($"There was an issue in the DirectorService updating the director: {ex.Message}");
            return false;
        }
    }

  /// <summary>
  /// Checks if the director exists in the database, removes it if it exists.
  /// </summary>
  /// <param name="entity">Object of DirectorEntity</param>
  /// <returns>True if object was deleted, else false.</returns>
    public bool Delete(DirectorEntity entity)
    {
        try
        {
            var result = SelectOne(entity.FirstName, entity.LastName);
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
            Debug.WriteLine($"There was an issue in the DirectorService removing the director: {ex.Message}");
            return false;
        }
    }
}
