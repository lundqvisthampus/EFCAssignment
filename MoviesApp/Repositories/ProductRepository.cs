using MoviesApp.Contexts;
using MoviesApp.Entities;


namespace MoviesApp.Repositories;

/// <summary>
/// Repository for the Product-entity, uses base-repository for all CRUD.
/// </summary>
public class ProductRepository : BaseRepository<Product>
{
    private readonly ProductCatalogContext _context;

    public ProductRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
