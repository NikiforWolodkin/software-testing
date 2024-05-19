using System.Text.Json;

namespace Catalog.Tests.Utilities;

public static class ConfigReader
{
    public static Dictionary<string, string>? ReadCookiesFromJson(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var cookies = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        return cookies;
    }
}
