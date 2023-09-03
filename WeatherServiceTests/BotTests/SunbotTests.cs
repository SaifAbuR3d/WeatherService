using WeatherService.PubSub.Bot;
using WeatherService;

namespace WeatherServiceTests.BotTests
{
    public class SunbotTests
    {
        [Fact]
        public void ProcessWeatherUpdate_Disabled_MustNotBeActivated()
        {
            var weatherData = new WeatherData { Temperature = 35 };
            var sut = new Sunbot
            {
                Enabled = false,
                TemperatureThreshold = 30,
                Message = "It's sunny!"
            };

            sut.ProcessWeatherUpdate(weatherData);

            Assert.False(sut.Activated);
        }

        [Fact]
        public void ProcessWeatherUpdate_EnabledWithTemperatureHigherThanTemperatureThreshold_MustBeActivated()
        {
            var weatherData = new WeatherData { Temperature = 35 };
            var sut = new Sunbot
            {
                Enabled = true,
                TemperatureThreshold = 30,
                Message = "It's sunny!"
            };

            sut.ProcessWeatherUpdate(weatherData);

            Assert.True(sut.Activated);
        }

        [Fact]
        public void ProcessWeatherUpdate_EnabledWithTemperatureLowerThanTemperatureThreshold_MustNotBeActivated()
        {
            var weatherData = new WeatherData { Temperature = 20 };
            var sut = new Sunbot
            {
                Enabled = true,
                TemperatureThreshold = 30,
                Message = "It's sunny!"
            };

            sut.ProcessWeatherUpdate(weatherData);

            Assert.False(sut.Activated);
        }
    }
}
