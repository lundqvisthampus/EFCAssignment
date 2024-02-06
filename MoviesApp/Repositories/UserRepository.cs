using MoviesApp.Contexts;
using MoviesApp.Entities;

namespace MoviesApp.Repositories;

public class UserRepository : BaseRepository<User>
{
    private readonly ProductCatalogContext _context;

    public UserRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
