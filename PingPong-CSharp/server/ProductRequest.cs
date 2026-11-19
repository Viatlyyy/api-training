namespace PingServer;

public sealed class ProductRequest
{
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public int? Quantity { get; init; }

    public Dictionary<string, string[]> Validate()
    {
        var errors = new Dictionary<string, string[]>();
        string name = Name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            errors["name"] = ["Введите название товара от 1 до 100 символов."];

        if (Price is null or <= 0 or > 1_000_000_000m)
            errors["price"] = ["Цена должна быть больше 0 и не больше 1 000 000 000."];
        else if (decimal.Round(Price.Value, 2) != Price.Value)
            errors["price"] = ["Укажите цену с точностью до двух знаков после запятой."];

        if (Quantity is null or < 0 or > 1_000_000)
            errors["quantity"] = ["Количество должно быть целым числом от 0 до 1 000 000."];

        return errors;
    }
}
