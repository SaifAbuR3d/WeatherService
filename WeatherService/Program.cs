using System.Text.Json;
using WeatherService.Config;
using WeatherService.PubSub;
using WeatherService.Strategies;

namespace WeatherService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Load Bot Configurations
            BotConfig? botConfig = LoadConfig();

            if (botConfig == null)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine("Exiting...");
                Thread.Sleep(2000);
                return;
            }
            #endregion

            #region Get Format Strategy
            Console.Write("Enter the path to the weather data file: ");
            string? weatherDataFilePath = Console.ReadLine();

            ITextFormatStrategy? textFormatStrategy = TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath);

            if (textFormatStrategy == null)
            {
                Console.WriteLine("Cannot Handle that File");
                Console.WriteLine("Exiting...");
                Thread.Sleep(2000);
                return;
            }
            #endregion

            string weatherDataText = File.ReadAllText(weatherDataFilePath);
            WeatherDataPublisher weatherDataPublisher = new WeatherDataPublisher(weatherDataText, textFormatStrategy, botConfig);

            WeatherData newData = new WeatherData
            {
                Humidity = 10000,
                Location = "New Jersy",
                Temperature = 20000
            }; 

            weatherDataPublisher.WeatherData = newData;

        }

        private static BotConfig? LoadConfig()
        {
            string filePath = "config.json";
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            BotConfig? botConfig = ConfigManager.LoadConfig(filePath, options);
            return botConfig;
        }
    }
}