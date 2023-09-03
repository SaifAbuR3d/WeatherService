#nullable disable

using WeatherService;

namespace WeatherService.PubSub.Bot
{
    public class Sunbot : IWeatherBotSubscriber
    {
        public bool Enabled { get; init; }
        public int TemperatureThreshold { get; init; }
        public string Message { get; init; }

        public void ProcessWeatherUpdate(WeatherData weatherData)
        {
            if (!Enabled) return;
            if (weatherData.Temperature > TemperatureThreshold)
            {
                Console.WriteLine("SunBot Activated!");
                Console.WriteLine($"SunBot: \"{Message}\"");
            }
        }
    }
}