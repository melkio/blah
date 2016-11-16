using System;

namespace Demo04.Common.Commands
{
    public interface ICommand
    {
        string Id { get; }
        CommandHeaders Headers { get; }
    }

    public abstract class Command : ICommand
    {
        public string Id { get; set; }
        public CommandHeaders Headers { get; set; }

        protected Command()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class CommandHeaders
    {
        public string Username { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
