using System.Linq;
using System.Threading.Tasks;
using Demo04.Common.Commands;
using MassTransit;
using MongoDB.Driver;
using Demo04.Common.Models;
using Demo04.Common.Messages;
using Demo04.Common;

namespace Demo04.Backend.Consumers
{
    class CreateRoomCommandConsumer : IConsumer<CreateRoomCommand>
    {
        public async Task Consume(ConsumeContext<CreateRoomCommand> context)
        {
            var command = context.Message;

            var roomAlreadyExists = Database.Rooms.AsQueryable()
                .Where(x => x.Name == command.Name)
                .Any();

            if (roomAlreadyExists)
            {
                var message = new CommandFailedMessage { CorrelationId = command.Id, ErrorMessage = $"Room with name {command.Name} already exists" };
                await context.Publish(message);
            }
            else
            {
                var room = new Room { Name = command.Name, Owner = command.Owner };
                await Database.Rooms.InsertOneAsync(room);

                var message = new CommandHandledMessage<CreateRoomCommand> { CorrelationId = command.Id };
                await context.Publish(message);
            }
        }
    }
}
