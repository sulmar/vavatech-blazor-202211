using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure;
public class InMemoryProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> _products;

    public InMemoryProductRepository()
    {
        _products = new Dictionary<int, Product>
        {
            {1, new Product {Id = 1, Name = "Product 1", Color = "Red", Price = 99.10m, Size = Size.M} },
            {2, new Product {Id = 2, Name = "Product 2", Color = "Green", Price = 199.00m, Size = Size.XL} },
            {3, new Product {Id = 3, Name = "Product 3", Color = "Green", Price = 20.99m, Size = Size.M} },
            {4, new Product {Id = 4, Name = "Product 4", Color = "Blue", Price = 200.01m, Size = Size.S} },
            {5, new Product {Id = 5, Name = "Product 5", Color = "Red", Price = 400.10m, Size = Size.S} },
        };
        // Typ anonimowy
        // var x = new { Title = "Test", Color = "Red", Price = 100m };

        // x.Color = "Blue"; // nie działa przypisanie
        
        Dictionary<int, Tag> tags = new()
        {
            {1, new Tag {Id = 1, Title = "Software" } },
            {2, new Tag {Id = 2, Title = "Clothing" } },
            {3, new Tag {Id = 3, Title = "Hardware" } },
        };

        _products[1].Tags = new List<Tag> { tags[1], tags[2] };
        _products[2].Tags = new List<Tag> { tags[1], tags[2], tags[3] };
        _products[3].Tags = new List<Tag> { tags[1], tags[3] };
        _products[4].Tags = new List<Tag> { tags[1], tags[2] };
        _products[5].Tags = new List<Tag> { tags[2] };
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_products.Values.AsEnumerable());
    }

    public Task<IEnumerable<Product>> GetByColor(string color)
    {
        return Task.FromResult(_products.Values.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<Product> GetById(int id)
    {
        return Task.FromResult(_products[id]);
    }
}
