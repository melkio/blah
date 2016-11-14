using System;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace Demo01
{
    [RoutePrefix("api/chat")]
    public class ChatController : ApiController
    {
        [Route]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWebSocketRequestAsync);
            }

            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        async Task ProcessWebSocketRequestAsync(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;
            while (true)
            {
                var buffer = new ArraySegment<byte>(new byte[8192]);
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    var message = $"You sent message: <strong>{Encoding.UTF8.GetString(buffer.Array, 0, result.Count)}</strong> at {DateTime.Now.ToString()}";
                    await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                    break;
            }
        }
    }
}