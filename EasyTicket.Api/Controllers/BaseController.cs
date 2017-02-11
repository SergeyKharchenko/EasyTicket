using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace EasyTicket.Api.Controllers {
    public abstract class BaseController : ApiController {
        protected HttpResponseMessage Json(string content) {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(content, Encoding.UTF8, "application/json");
            return response;
        }

        protected IHttpActionResult ValidationError() {
            return new InvalidModelStateResult(ModelState, this);
        }
    }
}