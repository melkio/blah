namespace Demo04.Common.Commands
{
    public class PostChatMessageCommand : Command
    {
        public string Account { get; set; }
        public string Room { get; set; }
        public string Text { get; set; }
    }
}
