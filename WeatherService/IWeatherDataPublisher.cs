using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    public interface IWeatherDataPublisher
    {
        public void Attach(IWeatherBotSubscriber observer);

        public void Detach(IWeatherBotSubscriber observer);

        public void Notify();
    }
}
