using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Infrastructure;

// var app = WebApplication.Create(args);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// GET api/products
app.MapGet("api/products", async (IProductRepository repository) => await repository.GetAllAsync());

// GET api/products/{id}
app.MapGet("api/products/{id:int:min(1)}", async (int id, IProductRepository repository) => await repository.GetById(id));


//app.MapGet("api/users", (HttpRequestMessage req, HttpResponseMessage res) 
//        => "Hello Users");

app.Run();
