using System.Web.Http;
using EasyTicket.Api.Data;
using EasyTicket.Api.Data.Dto;
using EasyTicket.Api.Data.Models;
using EasyTicket.Api.Infrastructure;

namespace EasyTicket.Api.Controllers {
    public class PlaceRequestController : ApiController {
        private readonly UzClient UZ;

        public PlaceRequestController() {
            UZ = new UzClient();
        }

        [HttpPost]
        [Route("api/Request")]
        public IHttpActionResult RequestPlace([FromBody]PlaceRequestDto placeRequestDto) {
            PlaceRequest request = Mapper.Map<PlaceRequestDto, PlaceRequest>(placeRequestDto);
            using (var context = new UzDbContext()) {
                context.PlaceRequests.Add(request);
                context.SaveChanges();
            }
            return Ok();
        }
    }
}
