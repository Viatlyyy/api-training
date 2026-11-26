namespace PingServer;

public static class ProductRequests
{
    private static string filePath = "..\\responses.txt";
    private static readonly object fileLock = new();

    public static IResult GetPing()
    {
        return Results.Ok(new { message = "pong" });
    }

    public static IResult PostProduct(ProductRequest product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            return Results.BadRequest(new { message = "Введите название товара." });
        if (product.Price <= 0)
            return Results.BadRequest(new { message = "Цена должна быть больше нуля." });
        if (decimal.Round(product.Price, 2) != product.Price)
            return Results.BadRequest(new { message = "Укажите цену с точностью до копеек." });
        if (product.Quantity < 0)
            return Results.BadRequest(new { message = "Количество не может быть отрицательным." });

        product.Name = product.Name.Trim();
        string productText = $"Название: {product.Name}" + Environment.NewLine
            + $"Цена: {product.Price:F2}" + Environment.NewLine
            + $"Количество: {product.Quantity}" + Environment.NewLine + Environment.NewLine;

        try
        {
            lock (fileLock)
            {
                File.AppendAllText(filePath, productText);
            }
            return Results.Ok(new { message = "Товар сохранён", product });
        }
        catch (Exception)
        {
            return Results.Problem("Не удалось записать товар в файл.");
        }
    }
}
