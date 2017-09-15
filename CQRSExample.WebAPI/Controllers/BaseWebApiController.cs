using System.Diagnostics;
using System.Web.Http;

namespace CQRSExample.WebAPI.Controllers
{
    public class BaseWebApiController : ApiController
    {
        public BaseWebApiController()
        {
            Debug.Print("Created");
        }
    }
}