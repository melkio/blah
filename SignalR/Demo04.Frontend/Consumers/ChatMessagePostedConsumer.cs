using System.Threading.Tasks;
using Demo04.Common.Messages;
using MassTransit;
using Microsoft.AspNet.SignalR;

namespace Demo04.Frontend.Consumers
{
    public class ChatMessagePostedConsumer : IConsumer<ChatMessagePostedMessage>
    {
        public Task Consume(ConsumeContext<ChatMessagePostedMessage> context)
        {
            var message = context.Message;

            var hub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

            return Task.WhenAll(
                hub.Clients.All.newMessageFromOthers(message.Room),
                hub.Clients.Group(message.Room).newMessage(message.Account, message.OccurredOn, message.Text));
        }
    }
}