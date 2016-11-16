namespace Demo04.Common.Commands
{
    public class PostMessageCommand : Command
    {
        public string RoomName { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
    }
}
