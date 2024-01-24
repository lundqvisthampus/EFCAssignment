using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesApp;
using MoviesApp.Contexts;
using MoviesApp.Repositories;


// Configuring dependency injection for the datacontext with connection string.
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<MovieDatabaseContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Coding\EC-code\CSharp\EFCAssignment\MoviesApp\Data\MovieDatabase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
    services.AddSingleton<DirectorRepository>();
    services.AddSingleton<GenreRepository>();
    services.AddSingleton<MovieProviderRepository>();
    services.AddSingleton<MovieRepository>();
    services.AddSingleton<ProductionCompanyRepository>();
}).Build();