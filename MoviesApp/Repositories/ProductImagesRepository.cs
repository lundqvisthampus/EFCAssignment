using MoviesApp.Contexts;
using MoviesApp.Entities;

namespace MoviesApp.Repositories;

public class ProductImagesRepository : BaseRepository<ProductImage>
{
    private readonly ProductCatalogContext _context;

    public ProductImagesRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
