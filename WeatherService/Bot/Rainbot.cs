#nullable disable

namespace WeatherService.Bot
{
    public class Rainbot : IWeatherBotSubscriber
    {
        public bool Enabled { get; init; }
        public int HumidityThreshold { get; init; }
        public string Message { get; init; }

        public void ProcessWeatherUpdate(IWeatherDataPublisher weatherDataPublisher)
        {
            if (!Enabled) return;
            if (((WeatherData)weatherDataPublisher).Humidity > HumidityThreshold)
            {
                Console.WriteLine($"RainBot: \"{Message}\"");
            }
        }
    }
}