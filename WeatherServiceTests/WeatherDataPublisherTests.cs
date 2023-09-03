using System;
using Xunit;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using WeatherService.PubSub;
using WeatherService.Strategies;
using WeatherService.Config;
using WeatherService.PubSub.Bot;

namespace WeatherService.Tests
{
    public class WeatherDataPublisherTests
    {
        private readonly IFixture fixture;

        public WeatherDataPublisherTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private WeatherDataPublisher CreateWeatherDataPublisher()
        {
            var mockTextFormatStrategy = new Mock<ITextFormatStrategy>();
            var text = fixture.Create<string>();
            var botConfig = fixture.Create<BotConfig>();

            mockTextFormatStrategy.Setup(x => x.GetWeatherData(text)).Returns(fixture.Create<WeatherData>());

            return new WeatherDataPublisher(text, mockTextFormatStrategy.Object, botConfig);
        }

        [Fact]
        public void WeatherDataPublisher_AttachSubscriber()
        {
            var sut = CreateWeatherDataPublisher();
            var mockSubscriber = new Mock<IWeatherBotSubscriber>();

            sut.Attach(mockSubscriber.Object);

            Assert.Contains(mockSubscriber.Object, sut.Subscribers);
        }

        [Fact]
        public void WeatherDataPublisher_DetachSubscriber()
        {
            var sut = CreateWeatherDataPublisher();
            var mockSubscriber = new Mock<IWeatherBotSubscriber>();

            sut.Attach(mockSubscriber.Object);
            sut.Detach(mockSubscriber.Object);

            Assert.DoesNotContain(mockSubscriber.Object, sut.Subscribers);
        }

        [Fact]
        public void WeatherDataPublisher_WeatherDataChanges_NotifiesNewSubscriberWithNewWeatherData()
        {
            var sut = CreateWeatherDataPublisher();
            var mockSubscriber = new Mock<IWeatherBotSubscriber>();
            sut.Attach(mockSubscriber.Object);

            var newWeatherData = fixture.Create<WeatherData>();
            sut.WeatherData = newWeatherData;

            mockSubscriber.Verify(
                subscriber => subscriber.ProcessWeatherUpdate(newWeatherData)
            );
        }

        [Fact]
        public void WeatherDataPublisher_WeatherDataDoesNotChange_DoesNotNotifyNewSubscriber()
        {
            var sut = CreateWeatherDataPublisher();
            var mockSubscriber = new Mock<IWeatherBotSubscriber>();
            sut.Attach(mockSubscriber.Object);

            mockSubscriber.Verify(
                subscriber => subscriber.ProcessWeatherUpdate(It.IsAny<WeatherData>()),
                Times.Never
            );
        }

        [Fact]
        public void InitializeSubscribers_AttachesBotsFromBotConfig()
        {
            var botConfig = new BotConfig
            {
                Rainbot = fixture.Create<Rainbot>(),
                Snowbot = fixture.Create<Snowbot>(),
                Sunbot = fixture.Create<Sunbot>()
            };

            var sut = CreateWeatherDataPublisher();
            sut.Subscribers.Clear(); 

            sut.InitializeSubscribers(botConfig);

            Assert.Contains(botConfig.Rainbot, sut.Subscribers);
            Assert.Contains(botConfig.Snowbot, sut.Subscribers);
            Assert.Contains(botConfig.Sunbot, sut.Subscribers);
            Assert.Equal(3, sut.Subscribers.Count);
        }



        [Fact]
        public void Notify_CallsProcessWeatherUpdateForAllSubscribers()
        {
            var sut = CreateWeatherDataPublisher();
            sut.Subscribers.Clear(); 

            var mockSubscribers = new List<Mock<IWeatherBotSubscriber>>();

            for (int i = 0; i < 3; i++)
            {
                var mockSubscriber = new Mock<IWeatherBotSubscriber>();
                mockSubscribers.Add(mockSubscriber);
                sut.Attach(mockSubscriber.Object);
            }

            sut.Notify();

            foreach (var mockSubscriber in mockSubscribers)
            {
                mockSubscriber.Verify(
                    subscriber => subscriber.ProcessWeatherUpdate(It.IsAny<WeatherData>()),
                    Times.Once
                );
            }
        }
    }
}
