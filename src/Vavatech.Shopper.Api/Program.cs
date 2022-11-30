using Bogus;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Infrastructure;
using Vavatech.Shopper.Infrastructure.Fakers;

// var app = WebApplication.Create(args);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<Faker<Product>, ProductFaker>();

builder.Services.AddSingleton<IDictionary<int, Product>>((sp) =>
{
    var faker = sp.GetRequiredService<Faker<Product>>();

    var products = faker.Generate(100).ToDictionary(p => p.Id);

    return products;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7031");
        policy.WithMethods(new string[] { "GET" });
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");

// GET api/products
app.MapGet("api/products", async (IProductRepository repository) => await repository.GetAllAsync());

// GET api/products/{id}
app.MapGet("api/products/{id:int:min(1)}", async (int id, IProductRepository repository) => await repository.GetById(id));


//app.MapGet("api/users", (HttpRequestMessage req, HttpResponseMessage res) 
//        => "Hello Users");

app.Run();
