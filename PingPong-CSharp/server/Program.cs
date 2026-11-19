using PingServer;

var app = WebApplication.Create(args);

app.MapGet("/ping", () => Results.Ok(new { message = "pong" }));

app.MapPost("/products", (ProductRequest product) =>
{
    if (string.IsNullOrWhiteSpace(product.Name))
        return Results.BadRequest(new { message = "Введите название товара." });
    if (product.Price <= 0)
        return Results.BadRequest(new { message = "Цена должна быть больше нуля." });
    if (product.Quantity < 0)
        return Results.BadRequest(new { message = "Количество не может быть отрицательным." });

    product.Name = product.Name.Trim();
    return Results.Ok(new { message = "Товар принят", product });
});

app.Run("http://127.0.0.1:8001");
