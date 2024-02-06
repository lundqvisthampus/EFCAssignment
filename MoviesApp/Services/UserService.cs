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

    // CREATE
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

    // READ
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

    // UPDATE
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

    // DELETE
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

