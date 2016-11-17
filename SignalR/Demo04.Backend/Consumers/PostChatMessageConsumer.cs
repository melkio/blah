using System.Threading.Tasks;
using Demo04.Common.Commands;
using MassTransit;
using Demo04.Common.Models;
using Demo04.Common.Messages;
using Demo04.Common;
using System;

namespace Demo04.Backend.Consumers
{
    class PostChatMessageConsumer : IConsumer<PostChatMessageCommand>
    {
        public async Task Consume(ConsumeContext<PostChatMessageCommand> context)
        {
            var command = context.Message;
            var now = DateTime.Now;

            var message = new ChatMessage
            {
                Account = command.Account,
                Room = command.Room,
                Text = command.Text,
                Date = now
            };
            await Database.ChatMessages.InsertOneAsync(message);

            var notification = new ChatMessagePostedMessage
            {
                CorrelationId = command.Id,
                Account = command.Account,
                Room = command.Room,
                Text = command.Text,
                OccurredOn = now
            };
            await context.Publish(notification);
        }
    }
}

