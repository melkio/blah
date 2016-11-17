using System;

namespace Demo04.Common.Commands
{
    public abstract class Command 
    {
        public string Id { get; set; }

        protected Command()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
