using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Demo02
{
    [RoutePrefix("api/chat")]
    public class ChatController : ApiController
    {
        [Route]
        [HttpGet]
        public HttpResponseMessage Get(string account)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                var handler = new ChatHandler(account);
                HttpContext.Current.AcceptWebSocketRequest(context => handler.ProcessWebSocketRequestAsync(context));
            }

            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
    }
}