using WeatherService.PubSub.Bot;
using WeatherService;

namespace WeatherServiceTests.BotTests
{
    public class RainBotTests
    {
        [Fact]
        public void ProcessWeatherUpdate_Disabled_MustNotBeActivated()
        {
            var weatherData = new WeatherData { Humidity = 60 };
            var sut = new Rainbot
            {
                Enabled = false,
                HumidityThreshold = 70,
                Message = "It's raining!"
            };
            sut.ProcessWeatherUpdate(weatherData);
            Assert.False(sut.Activated);
        }
        [Fact]

        public void ProcessWeatherUpdate_EnabledWithHumidityLowerThanHumidityThreshold_MustNotBeActivated()
        {
            var weatherData = new WeatherData { Humidity = 60 };
            var sut = new Rainbot
            {
                Enabled = true,
                HumidityThreshold = 100,
                Message = "It's raining!"
            };
            sut.ProcessWeatherUpdate(weatherData);
            Assert.False(sut.Activated);
        }
        [Fact]

        public void ProcessWeatherUpdate_EnabledWithHumidityHigherThanHumidityThreshold_MustBeActivated()
        {
            var weatherData = new WeatherData { Humidity = 60 };
            var sut = new Rainbot
            {
                Enabled = true,
                HumidityThreshold = 40,
                Message = "It's raining!"
            };
            sut.ProcessWeatherUpdate(weatherData);
            Assert.True(sut.Activated);
        }
    }
}
