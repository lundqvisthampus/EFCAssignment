using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApp.Entities;

public class MovieEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [Required]
    public int ReleaseYear { get; set; }

    [Required]
    [ForeignKey(nameof(ProductionCompanyEntity))]
    public int ProductionCompanyId { get; set; }

    [Required]
    [ForeignKey(nameof(GenreEntity))]
    public int GenreId { get; set; }

    [Required]
    [ForeignKey(nameof(DirectorEntity))]
    public int DirectorId { get; set; }

    [ForeignKey(nameof(MovieProviderEntity))]
    public int? MovieProviderId { get; set; }


    public virtual ProductionCompanyEntity ProductionCompany { get; set; } = null!;
    public virtual GenreEntity Genre { get; set; } = null!;
    public virtual DirectorEntity Director { get; set; } = null!;
    public virtual MovieProviderEntity MovieProvider { get; set; } = null!;
}