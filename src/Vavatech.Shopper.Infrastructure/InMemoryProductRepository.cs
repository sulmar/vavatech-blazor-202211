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
