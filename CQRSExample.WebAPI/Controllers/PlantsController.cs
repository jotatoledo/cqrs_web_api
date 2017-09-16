using CQRSExample.Model.MaterialNumber;
using CQRSExample.Model.Plant;
using CQRSExample.Model.WorkCenter;
using CQRSExample.WebAPI.Models.MaterialNumber;
using CQRSExample.WebAPI.Models.Plant;
using CQRSExample.WebAPI.Models.WorkCenter;
using MediatR;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CQRSExample.WebAPI.Controllers
{
    [RoutePrefix("plants")]
    public class PlantsController : BaseWebApiController
    {
        public PlantsController(IMediator mediator) : base(mediator)
        {
        }

        [Route("")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<PlantDetails>), Description = "Query completed")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> QueryPlants()
        {
            var result = await _mediator.Send(new Domain.Plants.List.Query());
            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(PlantDetails), Description = "New entry created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> Create([FromBody]PlantFormModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new Domain.Plants.Create.Command(model));
            var result = await _mediator.Send(new Domain.Plants.Details.Query(model.Id));
            return Created(new Uri(Request.RequestUri, model.Id), result);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlantDetails), Description = "Entry found")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetPlant(string id)
        {
            var result = await _mediator.Send(new Domain.Plants.Details.Query(id));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Entry deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseMessage> Delete(string id)
        {
            await _mediator.Send(new Domain.Plants.Delete.Command(id));
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlantDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdatePlant(string id, [FromBody]PlantFormModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new Domain.Plants.Update.Command(id, model));
            var result = await _mediator.Send(new Domain.Plants.Details.Query(model.Id));
            return Ok(result);
        }

        [HttpGet]
        [Route("{plantId}/work-centers")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<WorkCenterDetails>), Description = "Query completed")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult QueryPlantWorkCenters(string plantId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{plantId}/work-centers")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(WorkCenterDetails), Description = "Entry created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult CreateWorkCenterForPlant(string plantId, [FromBody]WorkCenterFormModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{plantId}/work-centers/{workCenterId}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(WorkCenterDetails), Description = "Entry found")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult GetWorkCenterFromPlant(string plantId, string workCenterId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{plantId}/work-centers/{workCenterId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Entry deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage DeleteWorkCenterFromPlant(string plantId, string workCenterId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{plantId}/work-centers/{workCenterId}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(WorkCenterDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult UpdateWorkCenterFromPlant(string plantId, string workCenterId, [FromBody]WorkCenterFormModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{plantId}/work-centers/{workCenterId}/material-numbers")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<MaterialNumberDetails>), Description = "Query completed")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult QueryMaterialNumbersFromWorkCenter(string plantId, string workCenterId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{plantId}/work-centers/{workCenterId}/material-numbers")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(WorkCenterDetails), Description = "Association created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult AssociateMaterialNumberToWorkCenter(string plantId, string workCenterId, [FromBody]string[] materialNumbers)
        {
            // TODO null string[]?
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{plantId}/work-centers/{workCenterId}/material-numbers/{materialNumberId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Dissociated")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpRequestMessage DissociateMaterialNumberFromWorkCenter(string plantId, string workCenterId, string materialNumberId)
        {
            throw new NotImplementedException();
        }
    }
}