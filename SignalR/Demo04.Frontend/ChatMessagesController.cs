using Demo04.Common;
using Demo04.Common.Commands;
using MongoDB.Driver;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo04.Frontend
{
    [RoutePrefix("api/messages")]
    public class ChatMessagesController : ApiController
    {
        [Route("{roomName}")]
        [HttpGet]
        public HttpResponseMessage Get(string roomName)
        {
            var messages = Database.ChatMessages.AsQueryable()
                .Where(x => x.Room == roomName)
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, messages.Reverse());
        }

        [Route]
        [HttpPost]
        public HttpResponseMessage Post(PostChatMessageCommand command)
        {
            ServiceBus.Publish(command);

            return Request.CreateResponse(HttpStatusCode.Created, command.Id);
        }
    }
}