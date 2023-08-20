#nullable disable

using WeatherService.Bot;

namespace WeatherService.Config
{
    public class BotConfig
    {
        public Rainbot RainBot { get; init; }
        public Sunbot SunBot { get; init; }
        public Snowbot SnowBot { get; init; }
    }
}