using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Infrastructure;
public class InMemoryProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> _products;   

    public InMemoryProductRepository(IDictionary<int, Product> products)
    {
        _products = products;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_products.Values.AsEnumerable());
    }

    public Task<IEnumerable<Product>> GetByColor(string color)
    {
        return Task.FromResult(_products.Values.Where(p => p.Color.Equals(color, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<IEnumerable<Product>> GetByContent(string content)
    {
        var results = _products.Values
            .Where(p => 
                p.Name.Equals(content, StringComparison.OrdinalIgnoreCase) 
               || p.Description.Contains(content));

        return Task.FromResult(results);
    }

    public Task<Product> GetById(int id)
    {
        return Task.FromResult(_products[id]);
    }
}
