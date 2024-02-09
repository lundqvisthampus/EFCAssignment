using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository for the ProductReview-entity, uses base-repository for all CRUD.
/// </summary>
public class ProductReviewRepository : BaseRepository<ProductReview>
{
    private readonly ProductCatalogContext _context;

    public ProductReviewRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Overrides the GetAllAsync method from the base-repo.
    /// Also includes the User-entity and Product-Entiy, isntead of just ProductReview entity.
    /// </summary>
    /// <returns>List of entities, or null if exception was thrown</returns>
    public async override Task<IEnumerable<ProductReview>> GetAllAsync()
    {
        try
        {
            var entityList = await _context.ProductReviews.Include(x => x.User).Include(x => x.Product).ToListAsync();
            return entityList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    /// <summary>
    /// Overrides the GetOneAsync method from the base-repo.
    /// Also includes the User-entity and Product-Entiy, isntead of just ProductReview entity.
    /// </summary>
    /// <param name="expression">Example (x => x.Id == Id)</param>
    /// <returns>Entity if succeeded, else null</returns>
    public async override Task<ProductReview> GetOneAsync(Expression<Func<ProductReview, bool>> expression)
    {
        try
        {
            var result = await _context.ProductReviews
                .Include(x => x.User)
                .Include(x => x.Product)
                    .ThenInclude(p => p.Category)
                .Include(x => x.Product)
                    .ThenInclude(p => p.ProductImage)
                .FirstOrDefaultAsync(expression);
            return result!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}
