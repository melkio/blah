using System.Threading.Tasks;
using Demo04.Common.Commands;
using MassTransit;
using Demo04.Common;
using Demo04.Common.Models;

namespace Demo04.Backend.Consumers
{
    public class CommandStoreConsumer : IConsumer<ICommand>
    {
        public Task Consume(ConsumeContext<ICommand> context)
        {
            var command = new CommandDescriptor
            {
                Id = context.Message.Id,
                Username = context.Message.Headers.Username,
                OccurredOn = context.Message.Headers.OccurredOn
            };

            return Database.Commands.InsertOneAsync(command);
        }
    }
}