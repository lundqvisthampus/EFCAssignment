using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Entities;

public class MovieProviderEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string ProviderName { get; set; } = null!;

    public virtual ICollection<MovieEntity> Movies { get; set; } = new HashSet<MovieEntity>();
}
