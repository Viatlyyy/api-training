using System.Globalization;
using System.Net.Http.Json;

namespace PingClient;

public partial class Form1 : Form
{
    private static readonly HttpClient http = new() { Timeout = TimeSpan.FromSeconds(5) };
    private string logPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "ApiTraining", "responses.txt");

    public Form1()
    {
        InitializeComponent();
        logPathTextBox.Text = logPath;
    }

    private async void sendButton_Click(object? sender, EventArgs e)
    {
        sendButton.Enabled = false;
        resultTextBox.Text = "Отправка товара…";
        saveStatusLabel.Text = "Ожидание ответа…";

        try
        {
            var product = new
            {
                name = productNameTextBox.Text.Trim(),
                price = decimal.Parse(priceNumericUpDown.Text),
                quantity = int.Parse(quantityNumericUpDown.Text, NumberStyles.Number)
            };

            using var response = await http.PostAsJsonAsync("http://127.0.0.1:8001/products", product);
            string answer = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}"
                + Environment.NewLine + await response.Content.ReadAsStringAsync();

            if (!IsDisposed)
                resultTextBox.Text = answer;

            Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);
            File.AppendAllText(logPath, answer + Environment.NewLine + Environment.NewLine);

            if (!IsDisposed)
                saveStatusLabel.Text = "Ответ дописан в файл:";
        }
        catch (Exception error)
        {
            if (!IsDisposed)
            {
                resultTextBox.AppendText(Environment.NewLine + "Ошибка: " + error.Message);
                saveStatusLabel.Text = "Новая запись не добавлена.";
            }
        }
        finally
        {
            if (!IsDisposed)
                sendButton.Enabled = true;
        }
    }
}
