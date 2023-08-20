using System.Text.Json;
using WeatherService.Config;

namespace WeatherService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "config.json"; 
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };


            BotConfig? botConfig = ConfigManager.LoadConfig(filePath, options);

            if (botConfig == null )
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine("Exiting...");
                Thread.Sleep(2000);
                return;
            }

            Console.WriteLine(JsonSerializer.Serialize(botConfig, options));

        }
    }
}