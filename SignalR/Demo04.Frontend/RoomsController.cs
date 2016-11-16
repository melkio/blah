using Demo04.Common;
using MongoDB.Driver;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo04.Frontend
{
    [RoutePrefix("api/rooms")]
    public class RoomsController : ApiController
    {
        [Route]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var rooms = Database.Rooms.AsQueryable().ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, rooms);
        }
    }
}