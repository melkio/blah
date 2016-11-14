using Microsoft.AspNet.SignalR;
using System;

namespace Demo03
{
    public class ChatHub : Hub
    {
        public void Send(string account, string message)
        {
            var content = $"<i>{account}</i>: <strong>{message}</strong> [{DateTime.Now.ToString()}]";
            Clients.All.broadcast(content);
        }
    }
}