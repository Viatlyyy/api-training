using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace PingClient;

public partial class Form1 : Form
{
    private const string ProductsUrl = "http://127.0.0.1:8001/products";

    private static readonly JsonSerializerOptions DisplayJson = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
    };

    private CancellationTokenSource? activeRequest;

    private static readonly HttpClient Http = new(new HttpClientHandler
    {
        UseProxy = false,
        AllowAutoRedirect = false
    })
    {
        Timeout = TimeSpan.FromSeconds(5)
    };

    public Form1()
    {
        InitializeComponent();
    }

    private async void sendButton_Click(object? sender, EventArgs e)
    {
        if (!sendButton.Enabled)
            return;

        inputErrors.Clear();
        string name = productNameTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            inputErrors.SetError(productNameTextBox, "Введите название от 1 до 100 символов.");
        bool validPrice = decimal.TryParse(priceNumericUpDown.Text, NumberStyles.Number,
            CultureInfo.CurrentCulture, out decimal price);
        if (!validPrice || price <= 0 || price > 1_000_000_000m || decimal.Round(price, 2) != price)
            inputErrors.SetError(priceNumericUpDown, "Введите цену больше 0, до 1 000 000 000, с точностью до копеек.");

        bool validQuantity = int.TryParse(quantityNumericUpDown.Text, NumberStyles.Number,
            CultureInfo.CurrentCulture, out int quantity);
        if (!validQuantity || quantity < 0 || quantity > 1_000_000)
            inputErrors.SetError(quantityNumericUpDown, "Введите целое количество от 0 до 1 000 000.");

        if (inputErrors.HasErrors)
        {
            resultTextBox.Text = "Проверьте данные товара. Запрос ещё не отправлен.";
            if (inputErrors.GetError(productNameTextBox).Length > 0)
                productNameTextBox.Focus();
            else if (inputErrors.GetError(priceNumericUpDown).Length > 0)
                priceNumericUpDown.Focus();
            else
                quantityNumericUpDown.Focus();
            return;
        }

        priceNumericUpDown.Value = price;
        quantityNumericUpDown.Value = quantity;
        var product = new
        {
            name,
            price,
            quantity
        };

        using var cancellation = new CancellationTokenSource();
        activeRequest = cancellation;
        SetInputsEnabled(false);
        resultTextBox.Text = "Отправка товара…";

        try
        {
            using HttpResponseMessage response = await Http.PostAsJsonAsync(ProductsUrl, product, cancellation.Token);
            string body = await response.Content.ReadAsStringAsync(cancellation.Token);
            if (IsDisposed || Disposing || cancellation.IsCancellationRequested)
                return;

            resultTextBox.Text = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}"
                + Environment.NewLine + Environment.NewLine + FormatJson(body);
        }
        catch (HttpRequestException)
        {
            if (!IsDisposed && !Disposing)
                resultTextBox.Text = "Не удалось связаться с API. Запустите сервер ASP.NET Core на порту 8001.";
        }
        catch (OperationCanceledException) when (!cancellation.IsCancellationRequested)
        {
            resultTextBox.Text = "Сервер не ответил за 5 секунд.";
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception error) when (error is InvalidOperationException or ArgumentException)
        {
            if (!IsDisposed && !Disposing)
                resultTextBox.Text = "Не удалось прочитать ответ сервера: " + error.Message;
        }
        finally
        {
            activeRequest = null;
            if (!IsDisposed && !Disposing)
                SetInputsEnabled(true);
        }
    }

    private void SetInputsEnabled(bool enabled)
    {
        sendButton.Enabled = enabled;
        productNameTextBox.Enabled = enabled;
        priceNumericUpDown.Enabled = enabled;
        quantityNumericUpDown.Enabled = enabled;
    }

    private static string FormatJson(string body)
    {
        try
        {
            using JsonDocument document = JsonDocument.Parse(body);
            return JsonSerializer.Serialize(document.RootElement, DisplayJson);
        }
        catch (JsonException)
        {
            return body;
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (!e.Cancel)
            activeRequest?.Cancel();
    }
}
