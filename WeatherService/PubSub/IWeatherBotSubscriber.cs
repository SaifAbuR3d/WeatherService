using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.PubSub
{
    public interface IWeatherBotSubscriber
    {
        public void ProcessWeatherUpdate(WeatherData weatherDataPublisher);
    }
}
