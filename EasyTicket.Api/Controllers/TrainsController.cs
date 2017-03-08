using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;

namespace EasyTicket.Api.Controllers {
    public class TrainsController : BaseController {
        private readonly UzClient UZ;

        public TrainsController() {
            UZ = new UzClient();
        }

        // POST api/trains
        public async Task<IHttpActionResult> Post([FromBody]TrainsRequestDto requestDto) {
            UzContext context = await UZ.GetUZContextAsync();
            TrainsResponse trains = await UZ.GetTrainsAsync(context, requestDto.StationFromId, requestDto.StationToId, requestDto.DateTime);
            return Json(trains);
        }
    }
}