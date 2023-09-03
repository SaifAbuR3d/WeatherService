#nullable disable

using System;
using WeatherService;

namespace WeatherService.PubSub.Bot
{
    public class Snowbot : IWeatherBotSubscriber
    {
        public bool Enabled { get; init; }
        public int TemperatureThreshold { get; init; }
        public string Message { get; init; }
        public bool Activated { get; private set; }

        public void ProcessWeatherUpdate(WeatherData weatherData)
        {
            if (!Enabled) return;
            if (weatherData.Temperature < TemperatureThreshold)
            {
                Activated = true;
                Console.WriteLine("Snowbot Activated!");
                Console.WriteLine($"Snowbot: \"{Message}\"");
            }
        }
    }
}