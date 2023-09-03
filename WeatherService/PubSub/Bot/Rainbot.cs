#nullable disable

using WeatherService;

namespace WeatherService.PubSub.Bot
{
    public class Rainbot : IWeatherBotSubscriber
    {
        public bool Enabled { get; init; }
        public int HumidityThreshold { get; init; }
        public string Message { get; init; }

        public void ProcessWeatherUpdate(WeatherData weatherData)
        {
            if (!Enabled) return;
            if (weatherData.Humidity > HumidityThreshold)
            {
                Console.WriteLine("Rainbot Activated!");
                Console.WriteLine($"Rainbot: \"{Message}\"");
            }
        }
    }
}