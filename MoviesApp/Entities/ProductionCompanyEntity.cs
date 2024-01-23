using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Entities;

public class ProductionCompanyEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string CompanyName { get; set; } = null!;

    public virtual ICollection<MovieEntity> Movies { get; set; } = new HashSet<MovieEntity>();
}
