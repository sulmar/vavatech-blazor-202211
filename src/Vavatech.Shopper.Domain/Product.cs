namespace Vavatech.Shopper.Domain;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public Size? Size { get; set; }
}

public enum Size
{
    S,
    M,
    L,
    XL
}