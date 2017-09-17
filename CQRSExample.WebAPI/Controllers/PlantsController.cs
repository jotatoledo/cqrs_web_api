using CQRSExample.Model.MaterialNumber;
using CQRSExample.Model.Plant;
using CQRSExample.Model.WorkCenter;
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
        public async Task<IHttpActionResult> QueryPlantWorkCenters(string plantId)
        {
            var result = await _mediator.Send(new Domain.WorkCenters.List.QueryFromPlant(plantId));
            return Ok(result);
        }

        [HttpPost]
        [Route("{plantId}/work-centers")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(WorkCenterDetails), Description = "Entry created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> CreateWorkCenterForPlant(string plantId, [FromBody]WorkCenterFormModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new Domain.WorkCenters.Create.Command(plantId, model));
            var result = await _mediator.Send(new Domain.WorkCenters.Details.Query(plantId, model.Id));
            return Created(new Uri(Request.RequestUri, model.Id), result);
        }

        [HttpGet]
        [Route("{plantId}/work-centers/{workCenterId}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(WorkCenterDetails), Description = "Entry found")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetWorkCenterFromPlant(string plantId, string workCenterId)
        {
            var result = await _mediator.Send(new Domain.WorkCenters.Details.Query(plantId, workCenterId));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{plantId}/work-centers/{workCenterId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Entry deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseMessage> DeleteWorkCenterFromPlant(string plantId, string workCenterId)
        {
            await _mediator.Send(new Domain.WorkCenters.Delete.Command(plantId, workCenterId));
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{plantId}/work-centers/{workCenterId}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(WorkCenterDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateWorkCenterFromPlant(
            string plantId,
            string workCenterId,
            [FromBody]WorkCenterFormModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _mediator.Send(new Domain.WorkCenters.Update.Command(plantId, workCenterId, model));
            var result = await _mediator.Send(new Domain.WorkCenters.Details.Query(plantId, model.Id));
            return Ok(result);
        }

        [HttpGet]
        [Route("{plantId}/work-centers/{workCenterId}/material-numbers")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<MaterialNumberDetails>), Description = "Query completed")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> QueryMaterialNumbersFromWorkCenter(string plantId, string workCenterId)
        {
            var result = await _mediator.Send(new Domain.WorkCenters.MaterialNumbers.List.Query(plantId, workCenterId));
            return Ok(result);
        }

        [HttpPost]
        [Route("{plantId}/work-centers/{workCenterId}/material-numbers")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(WorkCenterDetails), Description = "Association created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AssociateMaterialNumberToWorkCenter(
            string plantId,
            string workCenterId,
            [FromBody]string[] materialNumberId)
        {
            // TODO null string[]?
            await _mediator.Send(new Domain.WorkCenters.MaterialNumbers.Associate.Command(plantId, workCenterId, materialNumberId));
            var result = await _mediator.Send(new Domain.WorkCenters.Details.Query(plantId, workCenterId));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{plantId}/work-centers/{workCenterId}/material-numbers/{materialNumberId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Dissociated")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseMessage> DissociateMaterialNumberFromWorkCenter(
            string plantId,
            string workCenterId,
            string materialNumberId)
        {
            await _mediator.Send(new Domain.WorkCenters.MaterialNumbers.Dissociate.Command(plantId, workCenterId, materialNumberId));
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}