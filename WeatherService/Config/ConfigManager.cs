using System.Text.Json;
using WeatherService.PubSub;

namespace WeatherService.Config
{
    internal class ConfigManager
    {
        public static BotConfig? LoadConfig(string filePath, JsonSerializerOptions options)
        {
            string fileContent = File.ReadAllText(filePath);
            BotConfig? config = JsonSerializer.Deserialize<BotConfig>(fileContent, options);
            return config;
        }
    }
}
