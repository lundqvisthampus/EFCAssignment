using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Entities;

public class DirectorEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    public DateTime BirthDate { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new HashSet<MovieEntity>();
}
