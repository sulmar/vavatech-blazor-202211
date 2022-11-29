var app = WebApplication.Create(args);
app.MapGet("/", () => "Hello World!");

// GET api/products
app.MapGet("api/products", () => "Hello Products");

// GET api/products/{id}
app.MapGet("api/products/{id:int:min(1)}", (int id) => $"Hello Product {id}");


//app.MapGet("api/users", (HttpRequestMessage req, HttpResponseMessage res) 
//        => "Hello Users");

app.Run();
