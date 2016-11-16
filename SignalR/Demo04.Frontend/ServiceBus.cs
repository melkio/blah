using Demo04.Frontend.Consumers;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Demo04.Frontend
{
    public static class ServiceBus
    {
        private static IBusControl bus;

        public static void Initialize()
        {
            bus = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri("rabbitmq://localhost/chat"), h =>
                {
                    h.Username("admin");
                    h.Password("CodicePlastico");
                });

                config.ReceiveEndpoint(host, "frontend", endpoint =>
                {
                    endpoint.Consumer<CommandNotificationConsumer>();
                });
            });

            bus.Start();
        }

        public static Task Publish<T>(T message)
            where T : class
        {
            return bus.Publish(message);
        }
    }
}