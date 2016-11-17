using System;

namespace Demo04.Common.Models
{
    public class ChatMessage
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public string Room { get; set; }
        public string Text { get; set; }
        public DateTime Date {get;set;}
    }
}
