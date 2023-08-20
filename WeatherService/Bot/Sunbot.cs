#nullable disable

namespace WeatherService.Bot
{
    public class Sunbot : IWeatherBotSubscriber
    {
        public bool Enabled { get; init; }
        public int TemperatureThreshold { get; init; }
        public string Message { get; init; }

        public void ProcessWeatherUpdate(IWeatherDataPublisher weatherDataPublisher)
        {
            if (!Enabled) return;
            if ( ((WeatherData)weatherDataPublisher).Temperature > TemperatureThreshold )
            {
                Console.WriteLine($"SunBot: \"{Message}\"");
            }
        }
    }
}