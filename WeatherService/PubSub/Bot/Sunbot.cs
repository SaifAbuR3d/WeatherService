#nullable disable

namespace WeatherService.PubSub.Bot
{
    public class Sunbot : IWeatherBotSubscriber
    {
        public bool Enabled { get; init; }
        public int TemperatureThreshold { get; init; }
        public string Message { get; init; }
        public bool Activated { get; private set; }
        public void ProcessWeatherUpdate(WeatherData weatherData)
        {
            if (!Enabled) return;
            if (weatherData.Temperature > TemperatureThreshold)
            {
                Activated = true;
                Console.WriteLine("Sunbot Activated!");
                Console.WriteLine($"Sunbot: \"{Message}\"");
            }
        }
    }
}