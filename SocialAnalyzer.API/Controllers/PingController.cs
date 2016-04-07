using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialAnalyzer.API.Controllers
{
    public class PingController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Working!");
        }
    }
}