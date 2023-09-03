using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherService.Strategies
{
    public class JsonFormatStrategy : ITextFormatStrategy
    {
        public WeatherData GetWeatherData(string text)
        {
            WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(text);
            return weatherData; 
        }
    }
}
