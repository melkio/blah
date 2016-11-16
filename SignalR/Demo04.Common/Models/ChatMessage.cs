using System;

namespace Demo04.Common.Models
{
    public class ChatMessage
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public Room Room { get; set; }
        public string Text { get; set; }
        public DateTime Date {get;set;}
    }
}
