using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Config;
using WeatherService.Strategies;

namespace WeatherService.PubSub
{
    // publisher
    public class WeatherDataPublisher : IWeatherDataPublisher
    {

        private WeatherData _weatherData;  

        // any change in WeatherData from outside this class will automatically invoke Notify()
        public WeatherData WeatherData {
            set
            {
                _weatherData = value;
                Notify(); 
            }
        }

        private List<IWeatherBotSubscriber> _subscribers = new List<IWeatherBotSubscriber>();
        public WeatherDataPublisher(string text, ITextFormatStrategy textFormat, BotConfig botConfig)
        {
            InitializeSubscribers(botConfig);

            _weatherData = textFormat.GetWeatherData(text);

            Notify();
        }

        private void InitializeSubscribers(BotConfig botConfig)
        {
            Attach(botConfig.RainBot);
            Attach(botConfig.SnowBot);
            Attach(botConfig.SunBot);
        }

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
            Console.WriteLine("Notifying observers .... ");
            Thread.Sleep(500);
            foreach (var subscriber in _subscribers)
            {
                subscriber.ProcessWeatherUpdate(_weatherData);
                Thread.Sleep(500);
            }
        }
    }
}
