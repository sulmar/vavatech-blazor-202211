using System.Net.Http.Json;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.ClientApp.Services
{
    public class ProductService : IProductRepository
    {
        private readonly HttpClient client;

        public ProductService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await client.GetFromJsonAsync<IEnumerable<Product>>("/api/products");
        }

        public async Task<Product> GetProduct(int id)
        {
            return await client.GetFromJsonAsync<Product>($"/api/products/{id}");
        }

        public Task<IEnumerable<Product>> GetByColor(string color)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetByContent(string content)
        {
            return await client.GetFromJsonAsync<IEnumerable<Product>>($"/api/products?filter={content}");
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
