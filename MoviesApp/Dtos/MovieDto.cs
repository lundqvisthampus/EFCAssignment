using MoviesApp.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Dtos;

public class MovieDto
{
    public string Title { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public string DirectorFirstName { get; set; } = null!;
    public string DirectorLastName { get; set; } = null!;
    public DateTime DirectorBirthDate { get; set; }
    public string GenreName { get; set; } = null!;
    public string ProviderName { get; set; } = null!;
    public string ProductionCompanyName { get; set; } = null!;

}
