using System;
using System.Web.Http;

namespace CQRSExample.WebAPI.Controllers
{
    [RoutePrefix("work-centers")]
    public class WorkCentersController : BaseWebApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Query()
        {
            throw new NotImplementedException();
        }
    }
}