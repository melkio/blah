using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Demo04.Frontend
{
    public class CommandNotificationHub : Hub
    {
        public Task Register(string username)
        {
            return Groups.Add(Context.ConnectionId, username);
        }
    }
}