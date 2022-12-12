using Bogus;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Domain.Validators;
using Vavatech.Shopper.Infrastructure;
using Vavatech.Shopper.Infrastructure.Fakers;

// var app = WebApplication.Create(args);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<Faker<Product>, ProductFaker>();

builder.Services.AddSingleton<IValidator<Product>, ProductValidator>();

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
        //  policy.AllowAnyOrigin();
        //        policy.AllowAnyMethod();
        policy.WithOrigins("https://localhost:7031");
        policy.WithMethods(new string[] { "GET", "PUT" });
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");

// GET api/products
//app.MapGet("api/products", async (IProductRepository repository) => await repository.GetAllAsync());

// GET api/products/{id}
app.MapGet("api/products/{id:int:min(1)}", async (int id, IProductRepository repository) => await repository.GetById(id));


// Query String
// GET api/products?filter={filter}
app.MapGet("api/products/search", async ([FromQuery(Name ="filter")] string? filterValue, IProductRepository repository) =>
{
    if (filterValue == null)
        return await repository.GetAllAsync();
    else
        return await repository.GetByContent(filterValue);
});


// GET api/products?startindex={startindex}&PageSize={PageSize}
app.MapGet("/api/products", async (
    [FromQuery] int startIndex, 
    [FromQuery] int? pageSize,
    IProductRepository repository
    ) =>
{
    var productParameters = new PagingParameters
    {
        PageSize = pageSize.Value,
        StartIndex = startIndex
    };

    var products = await repository.Get(productParameters);

    return Results.Ok(products);

});

app.MapPut("api/products/{id}", async (
    [FromRoute] int id, 
    [FromBody] Product product, 
    [FromServices] IProductRepository repository,
    IValidator<Product> validator 
    ) =>
{
    if (id != product.Id)
        return Results.BadRequest();

    var result = await validator.ValidateAsync(product);

    if (!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }

    await repository.Update(product);

    return Results.NoContent();

});

//app.MapGet("api/users", (HttpRequestMessage req, HttpResponseMessage res) 
//        => "Hello Users");

// HEAD api/products/{barcode}

app.MapMethods("api/products/{barcode}", new string[] { "HEAD" }, async (string barcode, IProductRepository repository) =>
{
    if (await repository.Exists(barcode))
        return Results.Ok();
    else
        return Results.NotFound();

});

app.Run();
