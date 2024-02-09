using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Repositories;

/// <summary>
/// A base repository for all the other repositories to use.
/// All methods uses virtual so the other repos have the option to override the methods.
/// All methods are using async await to avoid the issue of the application locking up if the database querys takes too long.
/// </summary>
/// <typeparam name="TEntity">Type of entity of the repo that uses the base-repo.</typeparam>
public class BaseRepository<TEntity> where TEntity : class
{
    private readonly ProductCatalogContext _context;

    public BaseRepository(ProductCatalogContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tries to add one entity of the type that uses the base-repo, to the database.
    /// </summary>
    /// <param name="entity">Object of chosen type</param>
    /// <returns>Entity if succeeded, else null.</returns>
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            var result = await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to get one entity of the type that uses the base-repo, from the database.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>Entity if succeeded, else null.</returns>
    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            return result!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to get all entities of the type that uses the base-repo, from the database.
    /// </summary>
    /// <returns>List if succeeded, else null.</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entityList = await _context.Set<TEntity>().ToListAsync();
            return entityList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Tries to update entity of the type that uses the base-repo, in the database.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <param name="entity">Object of chosen type</param>
    /// <returns>True if updated, else false</returns>
    public virtual async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var findEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (findEntity != null)
            {
                _context.Entry(findEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
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


    /// <summary>
    /// Tries to delete entity of the type that uses the base-repo, from the database.
    /// </summary>
    /// <param name="entity">Object of chosen type</param>
    /// <returns>True if deleted, else false</returns>
    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
