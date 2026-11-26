using System.Globalization;
using System.Net.Http.Json;

namespace PingClient;

public partial class ProductForm : Form
{
    private static readonly HttpClient http = new() { Timeout = TimeSpan.FromSeconds(5) };
    private bool isSending;

    public ProductForm()
    {
        InitializeComponent();
    }

    private async void sendButton_Click(object? sender, EventArgs e)
    {
        if (isSending)
            return;

        try
        {
            var product = new
            {
                name = productNameTextBox.Text.Trim(),
                price = decimal.Parse(priceNumericUpDown.Text),
                quantity = int.Parse(quantityNumericUpDown.Text, NumberStyles.Number)
            };

            SetSending(true);
            resultTextBox.Text = "Отправка товара…";
            saveStatusLabel.Text = "Ожидание ответа сервера…";

            using var response = await http.PostAsJsonAsync("http://127.0.0.1:8001/products", product);
            resultTextBox.Text = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}"
                + Environment.NewLine + await response.Content.ReadAsStringAsync();
            saveStatusLabel.Text = response.IsSuccessStatusCode
                ? "Товар сохранён сервером."
                : "Товар не сохранён: ошибка сервера.";
        }
        catch (Exception error)
        {
            resultTextBox.Text = "Ошибка: " + error.Message;
            saveStatusLabel.Text = "Нет подтверждения о сохранении товара.";
        }
        finally
        {
            SetSending(false);
        }
    }

    private void SetSending(bool sending)
    {
        isSending = sending;
        foreach (Control control in Controls)
            control.Enabled = !sending;
        sendProgressBar.Enabled = true;
        sendProgressBar.Visible = sending;
        UseWaitCursor = sending;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (isSending)
            e.Cancel = true;
    }
}
