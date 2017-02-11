using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.Api.Infrastructure;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Models;

namespace EasyTicket.Api.Controllers {
    public class RequestController : BaseController {
        private readonly UzClient UZ;

        public RequestController() {
            UZ = new UzClient();
        }

        [HttpPost]
        [Route("api/Request")]
        public IHttpActionResult RequestPlace([FromBody]RequestDto requestDto) {
            if (!ModelState.IsValid) {
                return ValidationError();
            }
            Request request = Mapper.Map<RequestDto, Request>(requestDto);
            using (var context = new UzDbContext()) {
                context.Requests.Add(request);
                context.SaveChanges();
            }
            return Ok();
        }
    }
}