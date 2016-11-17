using Demo04.Backend.Consumers;
using MassTransit;
using System;

namespace Demo04.Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var host = config.Host(new Uri("rabbitmq://localhost/chat"), h =>
                {
                    h.Username("admin");
                    h.Password("CodicePlastico");
                });

                config.ReceiveEndpoint(host, "backend", endpoint =>
                {
                    endpoint.Consumer<PostChatMessageConsumer>();
                });
            });

            bus.Start();

            Console.ReadLine();

            bus.Stop();
        }
    }
}
