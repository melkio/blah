using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Akka.Actor;

namespace HttpCache.Items
{
    [RoutePrefix("api/items")]
    public partial class ItemsController : ApiController
    {
        [Route]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(PostModel payload)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            var message = new CreateItemRequest(payload.Code, payload.Description, payload.Value);
            var result = await ActorEnvironment.Current.ItemsGateway.Ask<CreateItemResponse>(message);

            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri($"api/items/{result.Id}", UriKind.Relative);
            response.Headers.ETag = new EntityTagHeaderValue(string.Concat("\"", result.ETag, "\"" ));

            return response;
        }
    }
}