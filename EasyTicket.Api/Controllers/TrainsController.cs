using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources;

namespace EasyTicket.Api.Controllers {
    public class TrainsController : BaseController {
        private readonly UzClient UZ;

        public TrainsController() {
            UZ = new UzClient();
        }

        // POST api/trains
        public async Task<HttpResponseMessage> Post([FromBody]TrainsRequestDto requestDto) {
            UzContext context = await UZ.GetUZContext();
            string trains = await UZ.GetTrains(context, requestDto.StationIdFrom, requestDto.StationIdTo, requestDto.DateTime);
            return Json(trains);
        }
    }
}