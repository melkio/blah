using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace Demo02
{
    class ChatHandler
    {
        private static ConcurrentDictionary<string, AspNetWebSocketContext> clients = new ConcurrentDictionary<string, AspNetWebSocketContext>();

        private readonly string account;

        public ChatHandler(string account)
        {
            this.account = account;
        }

        public async Task ProcessWebSocketRequestAsync(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;

            clients.AddOrUpdate(account, context, (account, current) => current);
            while (true)
            {
                var buffer = new ArraySegment<byte>(new byte[8192]);
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    var message = $"{account}: {Encoding.UTF8.GetString(buffer.Array, 0, result.Count)} [{DateTime.Now.ToString()}]";
                    await Broadcast(message);
                }
                else
                    break;
            }
        }

        private Task Broadcast(string message)
        {
            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            var tasks = clients.Values.Select(context => context.WebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None));

            return Task.WhenAll(tasks);
        }
    }
}