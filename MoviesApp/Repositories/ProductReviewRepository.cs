using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MoviesApp.Repositories;

public class ProductReviewRepository : BaseRepository<ProductReview>
{
    private readonly ProductCatalogContext _context;

    public ProductReviewRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }

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

    public async override Task<ProductReview> GetOneAsync(Expression<Func<ProductReview, bool>> expression)
    {
        try
        {
            var result = await _context.ProductReviews.Include(x => x.User).Include(x => x.Product).FirstOrDefaultAsync(expression);
            return result!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}
