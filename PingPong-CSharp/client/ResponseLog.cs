using System.Globalization;
using System.Text;

namespace PingClient;

internal sealed class ResponseLog
{
    private static readonly object SyncRoot = new();
    private static readonly Encoding Utf8 = new UTF8Encoding(false);

    public static string DefaultPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "ApiTraining", "responses.txt");

    public string FilePath { get; }

    public ResponseLog(string filePath)
    {
        FilePath = Path.GetFullPath(filePath);
    }

    public void Append(string requestUrl, string responseText)
    {
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss zzz", CultureInfo.InvariantCulture);
        string entry = Environment.NewLine + $"[{timestamp}] POST {requestUrl}"
            + Environment.NewLine + responseText + Environment.NewLine
            + new string('-', 60) + Environment.NewLine;

        lock (SyncRoot)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.AppendAllText(FilePath, entry, Utf8);
        }
    }
}
