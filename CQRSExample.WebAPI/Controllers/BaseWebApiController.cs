using MediatR;
using System;
using System.Diagnostics;
using System.Web.Http;

namespace CQRSExample.WebAPI.Controllers
{
    public class BaseWebApiController : ApiController
    {
        protected readonly IMediator _mediator;

        public BaseWebApiController(IMediator mediator)
        {
            if (mediator == null) throw new ArgumentNullException(nameof(mediator));
            _mediator = mediator;
            Debug.Print("Created");
        }
    }
}