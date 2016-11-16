using Demo04.Common.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo04.Frontend
{
    [RoutePrefix("api/commands")]
    public class CommandsController : ApiController
    {
        [Route]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] IDictionary<string, object> payload)
        {
            var command = new CreateRoomCommand
            {
                Headers = new CommandHeaders
                {
                    OccurredOn = DateTime.Now,
                    Username = (string)payload["owner"]
                },
                Name = (string)payload["name"],
                Owner = (string)payload["owner"]
            };

            ServiceBus.Publish(command);

            return Request.CreateResponse(HttpStatusCode.Created, new { CommandId = command.Id });
        }
    }
}