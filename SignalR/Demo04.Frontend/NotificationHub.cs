using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Demo04.Frontend
{
    public class NotificationHub : Hub
    {
        public Task Unjoin(string room)
        {
            return Groups.Remove(Context.ConnectionId, room);
        }

        public Task Join(string account, string room)
        {
            Clients.All.newAccount(account, room);
            return Groups.Add(Context.ConnectionId, room);
        }
    }
}