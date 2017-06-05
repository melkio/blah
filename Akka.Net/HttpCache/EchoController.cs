using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HttpCache
{
    [RoutePrefix("api/echo")]
    public class EchoController : ApiController
    {
        [Route]
        [HttpGet]
        public HttpResponseMessage Get(string text)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {Message = text});
        }
    }
}