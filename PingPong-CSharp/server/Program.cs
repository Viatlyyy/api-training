using PingServer;

var app = WebApplication.Create(args);

app.MapGet("/ping", () => Results.Ok(new { message = "pong" }));

app.MapPost("/products", (ProductRequest product) =>
{
    var errors = product.Validate();
    if (errors.Count > 0)
        return Results.ValidationProblem(errors, title: "Проверьте данные товара.");

    return Results.Ok(new
    {
        message = "Товар принят",
        product = new
        {
            name = product.Name?.Trim(),
            price = product.Price,
            quantity = product.Quantity
        }
    });
});

app.Run("http://127.0.0.1:8001");
