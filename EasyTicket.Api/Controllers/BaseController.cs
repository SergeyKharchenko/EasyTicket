using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace EasyTicket.Api.Controllers {
    public abstract class BaseController : ApiController {
        protected HttpResponseMessage Json(string content) {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(content, Encoding.UTF8, "application/json");
            return response;
        }
    }
}