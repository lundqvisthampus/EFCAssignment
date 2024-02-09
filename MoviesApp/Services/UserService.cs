using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Tries to add entity to DB if it doesnt already exist, using the userRepository.
    /// </summary>
    /// <param name="userDto">Object with the data needed to create a user</param>
    /// <returns>Entity if succeeded or already exists, else null</returns>
    public async Task<User> CreateAsync(UserDto userDto)
    {
        try
        {
            var existingUser = await _userRepository.GetOneAsync(x => x.UserName == userDto.UserName);
            if (existingUser == null)
            {
                await _userRepository.CreateAsync(new User { 
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    Password = userDto.Password
                });

                var result = await _userRepository.GetOneAsync(x => x.UserName == userDto.UserName);
                return result;
            }
            return existingUser;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to get entity from DB using the productImagesRepository.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>Entity if succeeded, else null</returns>
    public async Task<User> GetOneAsync(Expression<Func<User, bool>> expression)
    {
        try
        {
            var result = await _userRepository.GetOneAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }


    /// <summary>
    /// Tries to get all entities from DB using the _userRepository.
    /// </summary>
    /// <returns>List of User if succeeded, else null.</returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        try
        {
            var result = await _userRepository.GetAllAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to update entity in DB based an an expression and entity.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id) to find entity in DB</param>
    /// <param name="entity">Entity with the new values that the entity should update to.</param>
    /// <returns>True if updated, else false.</returns>
    public async Task<bool> UpdateAsync(Expression<Func<User, bool>> expression, User entity)
    {
        try
        {
            var result = await _userRepository.UpdateAsync(expression, entity);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Tries to delete entity from DB
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>True if deleted, else false.</returns>
    public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        try
        {
            var result = await _userRepository.GetOneAsync(expression);
            if (result != null)
            {
                await _userRepository.DeleteAsync(result);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}

