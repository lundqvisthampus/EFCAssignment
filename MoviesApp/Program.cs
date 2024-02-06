using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesApp;
using MoviesApp.Contexts;
using MoviesApp.Repositories;
using MoviesApp.Services;


// Configuring dependency injection for the datacontext with connection string.
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    // Two different databases, one for products, one for movies.
    services.AddDbContext<MovieDatabaseContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Coding\EC-code\CSharp\EFCAssignment\MoviesApp\Data\MovieDatabase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
    services.AddDbContext<ProductCatalogContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Coding\EC-code\CSharp\EFCAssignment\MoviesApp\Data\ProductCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));

    // Repositories for the movie database
    services.AddSingleton<DirectorRepository>();
    services.AddSingleton<GenreRepository>();
    services.AddSingleton<MovieProviderRepository>();
    services.AddSingleton<MovieRepository>();
    services.AddSingleton<ProductionCompanyRepository>();

    // Repositories for the product database
    services.AddSingleton<CategoryRepository>();
    services.AddSingleton<ProductImagesRepository>();
    services.AddSingleton<UserRepository>();
    services.AddSingleton<ProductRepository>();
    services.AddSingleton<ProductReviewRepository>();

    services.AddSingleton<DirectorService>();
    services.AddSingleton<GenreService>();
    services.AddSingleton<MovieProviderService>();
    services.AddSingleton<MovieService>();
    services.AddSingleton<ProductionCompanyService>();
    services.AddSingleton<ConsoleMenu>();
}).Build();

var menu = builder.Services.GetRequiredService<ConsoleMenu>();
await menu.ShowMenu();