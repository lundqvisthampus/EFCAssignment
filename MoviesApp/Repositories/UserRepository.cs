using MoviesApp.Contexts;
using MoviesApp.Entities;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository for the User-entity, uses base-repository for all CRUD.
/// </summary>
public class UserRepository : BaseRepository<User>
{
    private readonly ProductCatalogContext _context;

    public UserRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
