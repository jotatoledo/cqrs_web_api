using CQRSExample.Model.MaterialNumber;
using CQRSExample.WebAPI.Models.MaterialNumber;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MediatR;
using System.Threading.Tasks;

namespace CQRSExample.WebAPI.Controllers
{
    [RoutePrefix("material-numbers")]
    public class MaterialNumbersController : BaseWebApiController
    {
        public MaterialNumbersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<MaterialNumberDetails>), Description = "Query completed")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult Query()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(MaterialNumberDetails), Description = "Entry created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Create([FromBody]MaterialNumberFormModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new Domain.MaterialNumbers.Create.Command(model));
            var result = await _mediator.Send(new Domain.MaterialNumbers.Get.Query(model.Id));
            return Created(new Uri(Request.RequestUri, model.Id), result);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(string id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MaterialNumberDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult Get(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MaterialNumberDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult Update(string id, [FromBody]MaterialNumberFormModel model)
        {
            throw new NotImplementedException();
        }
    }
}