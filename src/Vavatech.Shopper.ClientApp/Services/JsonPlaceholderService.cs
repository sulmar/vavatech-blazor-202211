using System.Net.Http.Json;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.ClientApp.Services
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient client;
        public JsonPlaceholderService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await client.GetFromJsonAsync<IEnumerable<User>>("/users");
        }
    }
}
