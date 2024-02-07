namespace MoviesApp.Dtos;

public class ProductReviewDto
{
    public int Rating { get; set; }
    public string UserName { get; set; } = null!;
    public string ProductName { get; set; } = null!;
}
