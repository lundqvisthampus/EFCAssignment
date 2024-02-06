using MoviesApp.Contexts;
using MoviesApp.Entities;


namespace MoviesApp.Repositories;

public class ProductRepository : BaseRepository<Product>
{
    private readonly ProductCatalogContext _context;

    public ProductRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
