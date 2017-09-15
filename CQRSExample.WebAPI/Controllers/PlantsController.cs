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
        public IHttpActionResult QueryPlants()
        {
            throw new NotImplementedException();
        }

        [Route("")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(PlantDetails), Description = "New entry created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult Create([FromBody]PlantFormModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlantDetails), Description = "Entry found")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult GetPlant(string id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Entry deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public HttpResponseMessage Delete(string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PlantDetails), Description = "Entry updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Wrong format/content of request body")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "No matching entry for the given Id")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult UpdatePlant(string id, [FromBody]PlantFormModel model)
        {
            throw new NotImplementedException();
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