using System.ComponentModel.DataAnnotations;

namespace Vavatech.Shopper.Domain;

public class Product : BaseEntity
{
    [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "Długość nieprawidłowa")]
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    [Range(1, 1000)]
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal? Discount { get; set; }
    public Size? Size { get; set; }
    public IEnumerable<Tag> Tags { get; set; }
}

public class Tag : BaseEntity
{
    public string Title { get; set; }
}

public enum Size
{
    S,
    M,
    L,
    XL
}