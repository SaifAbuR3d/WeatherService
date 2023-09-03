using FluentAssertions;
using WeatherService;
using WeatherService.Strategies;

namespace WeatherServiceTests
{
    public class TextFormatStrategyFactoryTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetTextFormatStrategy_EmptyOrNullString_ThrowsException(string? invalidWeatherDataFilePath)
        {
            Action act = () => TextFormatStrategyFactory.GetTextFormatStrategy(invalidWeatherDataFilePath);

            act.Should().Throw<ArgumentNullException>().WithParameterName("weatherDataFilePath");

        }
        [Fact]
        public void GetTextFormatStrategy_UnsupportedFileExtension_ReturnsNull()
        {
            string weatherDataFilePath = "x.doc";

            TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath)
                                 .Should().BeNull();
        }

        [Fact]
        public void GetTextFormatStrategy_JsonFileExtension_ReturnsJsonStrategy()
        {
            string weatherDataFilePath = "x.json";

            TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath)
                                .Should().BeOfType<JsonFormatStrategy>();
        }

        [Fact]
        public void GetTextFormatStrategy_XmlFileExtension_ReturnsXmlStrategy()
        {
            string weatherDataFilePath = "x.xml";

            TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath)
                                .Should().BeOfType<XmlFormatStrategy>();
        }
    }
}
