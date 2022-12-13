using Bogus;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text;
using Vavatech.Shopper.Api.Authorization;
using Vavatech.Shopper.Api.AuthorizationHandler;
using Vavatech.Shopper.Api.Hubs;
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
        policy.WithMethods(new string[] { "GET", "PUT", "POST" });
        policy.AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Adult", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("developer");
        policy.Requirements.Add(new MinimumAgeRequirment(18));
    });
});

// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.11
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    string secretKey = builder.Configuration["Secret-Key"];
    string issuer = "shopper.vavatech.pl";
    string audience = "domain.com";

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["X-Access-Token"];

            return Task.CompletedTask;
        }
    };

});


builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeAuthorizationHandler>();
builder.Services.AddSignalR();

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
    IProductRepository repository,
    HttpContext httpContext
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

app.MapPost("api/products", async (Product product, IHubContext<ProductsHub> hubContext) =>
{
    await hubContext.Clients.All.SendAsync("AddedProduct", product);
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

app.MapGet("api/reports/{id}", (int id) =>
{
    string path = Path.Combine(app.Environment.ContentRootPath, "Assets", "lorem-ipsum.pdf");

    if (!File.Exists(path)) 
        return Results.NotFound();

    Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

    return Results.File(stream, MediaTypeNames.Application.Pdf);
    
    // return Results.File(path, MediaTypeNames.Application.Pdf); 
});

app.MapControllers();

app.MapHub<ProductsHub>("ws/products");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
