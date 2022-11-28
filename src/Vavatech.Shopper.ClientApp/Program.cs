using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Vavatech.Shopper.ClientApp;
using Vavatech.Shopper.ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


// dotnet add package Microsoft.Extensions.Http --version 6.0.0
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com") });

// Rejestracja nazwanego klienta Http (Named HttpClient)
builder.Services.AddHttpClient<JsonPlaceholderService>(sp =>
{
    sp.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
});

await builder.Build().RunAsync();
