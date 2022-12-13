using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Api.Hubs
{
    public class ProductsHub : Hub
    {
        private readonly ILogger<ProductsHub> logger;

        public ProductsHub(ILogger<ProductsHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            // zła praktyka
            // logger.LogInformation($"Connected ConnectionId: {Context.ConnectionId}");

            // dobra praktyka
            logger.LogInformation("Connected ConnectionId: {ConnectionId}", Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async Task SendAddedProduct(Product product)
        {
            await this.Clients.Others.SendAsync("AddedProduct", product);
        }

        
    }
}
