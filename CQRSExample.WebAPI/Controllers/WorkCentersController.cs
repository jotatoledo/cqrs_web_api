using System.Web.Http;
using MediatR;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Collections.Generic;
using CQRSExample.Model.WorkCenter;
using System.Threading.Tasks;

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
        public async Task<IHttpActionResult> Query()
        {
            var result = await _mediator.Send(new Domain.WorkCenters.List.Query());
            return Ok(result);
        }
    }
}