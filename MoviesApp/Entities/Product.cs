using System;
using System.Collections.Generic;

namespace MoviesApp.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int ProductImageId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ProductImage ProductImage { get; set; } = null!;

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
}
