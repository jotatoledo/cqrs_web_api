using System;
using System.Web.Http;
using MediatR;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Collections.Generic;
using CQRSExample.Model.WorkCenter;

namespace CQRSExample.WebAPI.Controllers
{
    [RoutePrefix("work-centers")]
    public class WorkCentersController : BaseWebApiController
    {
        public WorkCentersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<WorkCenterDetails>), Description = "Query completed")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult Query()
        {
            throw new NotImplementedException();
        }
    }
}