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

        public List<IWeatherBotSubscriber> Subscribers { get => _subscribers; set => _subscribers = value; } 

        private List<IWeatherBotSubscriber> _subscribers = new List<IWeatherBotSubscriber>();
        public WeatherDataPublisher(string text, ITextFormatStrategy textFormatStrategy , BotConfig botConfig)
        {
            InitializeSubscribers(botConfig);

            _weatherData = textFormatStrategy.GetWeatherData(text);

            Notify();
        }

        public void InitializeSubscribers(BotConfig botConfig)
        {
            Attach(botConfig.Rainbot);
            Attach(botConfig.Snowbot);
            Attach(botConfig.Sunbot);
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
