using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Repositories;

public class BaseRepository<TEntity> where TEntity : class
{
    private readonly ProductCatalogContext _context;

    public BaseRepository(ProductCatalogContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task<TEntity> CreateAsync(TEntity entity)
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

    // READ
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression)
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

    public async Task<IEnumerable<TEntity>> GetAllAsync()
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
    
    // UPDATE
   public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
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
            return false!;
        }
    }


    // DELETE
    public async Task<bool> DeleteAsync(TEntity entity)
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
            return false!;
        }
    }
}
