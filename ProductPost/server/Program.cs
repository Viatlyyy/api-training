using PingServer;

var app = WebApplication.Create(args);

app.MapGet("/ping", ProductRequests.GetPing);
app.MapPost("/products", ProductRequests.PostProduct);

app.Run("http://127.0.0.1:8001");
