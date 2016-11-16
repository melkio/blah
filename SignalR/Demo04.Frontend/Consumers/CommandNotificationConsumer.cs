using System.Threading.Tasks;
using Demo04.Common.Messages;
using MassTransit;
using Microsoft.AspNet.SignalR;
using Demo04.Common;
using MongoDB.Driver;
using Demo04.Common.Models;

namespace Demo04.Frontend.Consumers
{
    public class CommandNotificationConsumer : IConsumer<CommandHandledMessage>, IConsumer<CommandFailedMessage>
    {
        public async Task Consume(ConsumeContext<CommandFailedMessage> context)
        {
            var message = context.Message;
            var descriptor = await GetSignalRGroupForCommand(message.CorrelationId);

            var hub = GlobalHost.ConnectionManager.GetHubContext<CommandNotificationHub>();
            hub.Clients.Group(descriptor.Username).commandFailed(message.CorrelationId);
        }

        public async Task Consume(ConsumeContext<CommandHandledMessage> context)
        {
            var message = context.Message;
            var descriptor = await GetSignalRGroupForCommand(message.CorrelationId);

            var hub = GlobalHost.ConnectionManager.GetHubContext<CommandNotificationHub>();
            hub.Clients.Group(descriptor.Username).commandHandled(message.CorrelationId);
        }

        Task<CommandDescriptor> GetSignalRGroupForCommand(string commandId)
        {
            var filter = Builders<CommandDescriptor>.Filter.Eq(x => x.Id, commandId);
            return Database.Commands.Find(filter).SingleAsync();
        }
    }
}