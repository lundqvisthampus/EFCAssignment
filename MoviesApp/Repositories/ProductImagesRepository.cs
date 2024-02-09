using MoviesApp.Contexts;
using MoviesApp.Entities;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository for the ProductImage-entity, uses base-repository for all CRUD.
/// </summary>
public class ProductImagesRepository : BaseRepository<ProductImage>
{
    private readonly ProductCatalogContext _context;

    public ProductImagesRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
