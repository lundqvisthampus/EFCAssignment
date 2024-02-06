namespace MoviesApp.Dtos;

public class ProductDto
{
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}
