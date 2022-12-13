using Vavatech.AuthApi.Domain;
using Vavatech.AuthApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

builder.Services.AddSingleton<IEnumerable<User>>(sp =>
{
    var users = new List<User>()
    {
        new User { Id = 1, Username = "marcin", HashedPassword = "123", Email = "marcin.sulecki@sulmar.pl"},
        new User { Id = 2, Username = "john", HashedPassword = "321", Email = "john@domain.com"},
        new User { Id = 3, Username = "ann", HashedPassword = "123", Email = "ann@domain.com"},
    };

    return users;
});



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
