using System.Net.Http.Json;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.ClientApp.Services
{
    public class ProductService 
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
    }
}
