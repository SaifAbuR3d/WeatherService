using Xunit;
using WeatherService.PubSub.Bot;
using WeatherService;

namespace WeatherServiceTests.BotTests
{
    public class SnowbotTests
    {
        [Fact]
        public void ProcessWeatherUpdate_Disabled_MustNotBeActivated()
        {
            var weatherData = new WeatherData { Temperature = -5 };
            var sut = new Snowbot
            {
                Enabled = false,
                TemperatureThreshold = 0,
                Message = "It's snowing!"
            };

            sut.ProcessWeatherUpdate(weatherData);

            Assert.False(sut.Activated);
        }

        [Fact]
        public void ProcessWeatherUpdate_EnabledWithTemperatureLowerThanTemperatureThreshold_MustBeBeActivated()
        {
            var weatherData = new WeatherData { Temperature = 0 };
            var sut = new Snowbot
            {
                Enabled = true,
                TemperatureThreshold = 10,
                Message = "It's snowing!"
            };

            sut.ProcessWeatherUpdate(weatherData);

            Assert.True(sut.Activated);
        }

        [Fact]
        public void ProcessWeatherUpdate_EnabledWithTemperatureHigherThanTemperatureThreshold_MustNotBeBeActivated()
        {
            var weatherData = new WeatherData { Temperature = 20 };
            var sut = new Snowbot
            {
                Enabled = true,
                TemperatureThreshold = 0,
                Message = "It's snowing!"
            };

            sut.ProcessWeatherUpdate(weatherData);

            Assert.False(sut.Activated);
        }
    }
}
