using System.Net.Http.Json;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.ClientApp.Services
{
    public interface IProductService : IProductRepository
    {
        Task RecalcuateAllProductPriceAsync(decimal ratio);
    }

    public class ProductService : IProductService
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
            return await client.GetFromJsonAsync<IEnumerable<Product>>($"/api/products/search?filter={content}");
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetById(int id)
        {
            return await client.GetFromJsonAsync<Product>($"/api/products/{id}");
        }

        public Task RecalcuateAllProductPriceAsync(decimal ratio)
        {
            throw new NotImplementedException();
        }

        public async Task<VirtualizeResponse<Product>> Get(PagingParameters parameters)
        {
            string request = $"/api/products?StartIndex={parameters.StartIndex}&PageSize={parameters.PageSize}";

            var response = await client.GetFromJsonAsync<VirtualizeResponse<Product>>(request);

            return response;

        }

        public async Task Update(Product entity)
        {
            await client.PutAsJsonAsync($"api/products/{entity.Id}", entity);
        }

        public async Task<bool> Exists(string barcode)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, $"api/products/{barcode}");

            var response = await client.SendAsync(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;


        }
    }
}
