using MoviesApp.Contexts;
using MoviesApp.Entities;

namespace MoviesApp.Repositories;

public class CategoryRepository : BaseRepository<Category>
{
    private readonly ProductCatalogContext _context;

    public CategoryRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
