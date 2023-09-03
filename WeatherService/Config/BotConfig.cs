#nullable disable

using WeatherService.PubSub.Bot;

namespace WeatherService.Config
{
    public class BotConfig
    {
        public Rainbot Rainbot { get; init; }
        public Sunbot Sunbot { get; init; }
        public Snowbot Snowbot { get; init; }
    }
}