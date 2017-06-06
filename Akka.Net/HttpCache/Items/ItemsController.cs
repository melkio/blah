﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Akka.Actor;
using HttpCache.Items.Messages;

namespace HttpCache.Items
{
    [RoutePrefix("api/items")]
    public partial class ItemsController : ApiController
    {
        [Route("{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var message = new GetItemRequest(id, $"{Request.Headers.IfMatch}");
            var result = await ActorEnvironment.Current.ItemsGateway.Ask<GetItemResponse>(message);

            var model = new GetModel
            {
                Id = result.Id,
                Code = result.Code,
                Description = result.Description,
                Value = result.Value
            };
            var response = Request.CreateResponse(HttpStatusCode.OK, model);
            response.Headers.ETag = new EntityTagHeaderValue(string.Concat("\"", result.ETag, "\""));

            return response;
        }

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