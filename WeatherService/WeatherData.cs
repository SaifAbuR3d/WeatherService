using System.Xml;
using System.Xml.Serialization;

namespace WeatherService
{
    [XmlRoot("WeatherData")]
    public class WeatherData
    {
        [XmlElement("Location")]
        public string Location { get; init; }

        [XmlElement("Humidity")]
        public double Humidity { get; init; }
        [XmlElement("Temperature")]
        public double Temperature { get; init; }
    }
}