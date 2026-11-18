using System.Net.Http;

namespace PingClient;

public partial class Form1 : Form
{
    private const string PingUrl = "http://127.0.0.1:8001/ping";

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

    private async void pingButton_Click(object? sender, EventArgs e)
    {
        pingButton.Enabled = false;
        resultTextBox.Text = "Ожидание ответа…";

        try
        {
            using HttpResponseMessage response = await Http.GetAsync(PingUrl);
            string body = await response.Content.ReadAsStringAsync();
            resultTextBox.Text = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}"
                + Environment.NewLine + body;
        }
        catch (HttpRequestException)
        {
            resultTextBox.Text = "Не удалось связаться с API. Проверьте, что сервер ASP.NET Core запущен на порту 8001.";
        }
        catch (OperationCanceledException)
        {
            resultTextBox.Text = "Сервер не ответил за 5 секунд.";
        }
        finally
        {
            pingButton.Enabled = true;
        }
    }
}
