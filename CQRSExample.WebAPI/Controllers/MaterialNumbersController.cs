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
using CQRSExample.Model.WorkCenter;

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
        public async Task<IHttpActionResult> Query()
        {
            var result = await _mediator.Send(new Domain.MaterialNumbers.List.Query());
            return Ok(result);
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
            var result = await _mediator.Send(new Domain.MaterialNumbers.Details.Query(model.Id));
            return Created(new Uri(Request.RequestUri, model.Id), result);
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Entry deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseMessage> Delete(string id)
        {
            await _mediator.Send(new Domain.MaterialNumbers.Delete.Command(id));
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MaterialNumberDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Get(string id)
        {
            var result = await _mediator.Send(new Domain.MaterialNumbers.Details.Query(id));
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MaterialNumberDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Update(string id, [FromBody]MaterialNumberFormModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new Domain.MaterialNumbers.Update.Command(id, model));
            var result = await _mediator.Send(new Domain.MaterialNumbers.Details.Query(model.Id));
            return Ok(result);
        }

        [HttpGet]
        [Route("{materialNumberId}/work-centers")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<WorkCenterDetails>), Description = "Query completed")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetAssignedWorkCenters(string materialNumberId)
        {
            var result = await _mediator.Send(new Domain.MaterialNumbers.WorkCenters.List.Query(materialNumberId));
            return Ok(result);
        }

        [HttpPost]
        [Route("{materialNumberId}/work-centers")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MaterialNumberDetails), Description = "Association(s) created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AssignWorkCenter(string materialNumberId, [FromBody]IEnumerable<string> workCenterId)
        {
            await _mediator.Send(new Domain.MaterialNumbers.WorkCenters.Associate.Command(materialNumberId, workCenterId));
            var result = await _mediator.Send(new Domain.MaterialNumbers.Details.Query(materialNumberId));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{materialNumberId}/work-centers/{workCenterId}")]
        public async Task<HttpResponseMessage> DissociateWorkCenter(string materialNumberId, string workCenterId)
        {
            await _mediator.Send(new Domain.MaterialNumbers.WorkCenters.Dissociate.Command(materialNumberId, workCenterId));
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}