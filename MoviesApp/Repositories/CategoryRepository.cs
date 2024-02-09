using MoviesApp.Contexts;
using MoviesApp.Entities;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository for the category-entity, uses base-repository for all CRUD.
/// </summary>
public class CategoryRepository : BaseRepository<Category>
{
    private readonly ProductCatalogContext _context;

    public CategoryRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
