using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoviesApp.Entities;
namespace MoviesApp.Contexts;


/// <summary>
/// DbContext is responisble for interacting with the database.
/// Following code is used for creating the tables in the database
/// </summary>
public class MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options) : DbContext(options)
{
    public virtual DbSet<ProductionCompanyEntity> ProductionCompanies { get; set; }
    public virtual DbSet<GenreEntity> Genres { get; set; }
    public virtual DbSet<DirectorEntity> Directors { get; set; }
    public virtual DbSet<MovieProviderEntity> MovieProviders { get; set; }
    public virtual DbSet<MovieEntity> Movies { get; set; }


    /// <summary>
    /// Makes sure that the chosen properties of each entity is unique in the database.
    /// Also allows the MovieProviderId to be null.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionCompanyEntity>()
            .HasIndex(x => x.CompanyName)
            .IsUnique();

        modelBuilder.Entity<GenreEntity>()
            .HasIndex(x => x.GenreName)
            .IsUnique();

        modelBuilder.Entity<MovieProviderEntity>()
            .HasIndex(x => x.ProviderName)
            .IsUnique();

        modelBuilder.Entity<MovieEntity>()
            .Property(x => x.MovieProviderId)
            .IsRequired(false);
    }
}
