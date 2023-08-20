namespace WeatherService.Strategies
{
    public interface ITextFormatStrategy
    {
        public WeatherData GetWeatherData(string text); 
    }
}