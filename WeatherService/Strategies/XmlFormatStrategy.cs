using System.Xml.Serialization;

namespace WeatherService.Strategies
{
    public class XmlFormatStrategy : ITextFormatStrategy
    {
        public WeatherData GetWeatherData(string text)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(WeatherData));

            using (TextReader reader = new StringReader(text))
            {
                 return (WeatherData)serializer.Deserialize(reader);
            }

        }

    }
}
