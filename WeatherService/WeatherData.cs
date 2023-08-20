using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    // publisher
    internal class WeatherData : IWeatherDataPublisher
    {
        public int Humidity { get; init; }
        public int Temperature { get; init; }

        private List<IWeatherBotSubscriber> _subscribers = new List<IWeatherBotSubscriber>();

        public void Attach(IWeatherBotSubscriber observer)
        {
            _subscribers.Add(observer);
        }

        public void Detach(IWeatherBotSubscriber observer)
        {
            _subscribers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.ProcessWeatherUpdate(this); 
            }
        }
    }
}
