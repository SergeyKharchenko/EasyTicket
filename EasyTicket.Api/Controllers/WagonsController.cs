using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Infrastructure;

namespace EasyTicket.Api.Controllers {
    public class WagonsController : BaseController {
        private readonly UzClient UZ;

        public WagonsController() {
            UZ = new UzClient();
        }

        // POST api/wagons
        public async Task<HttpResponseMessage> Post([FromBody]WagonsRequestDto requestDto) {
            UzContext context = await UZ.GetUZContext();
            string wagons = await UZ.GetWagons(context, requestDto.StationFromId, requestDto.StationIdTo, requestDto.DateTime,
                                               requestDto.TrainId, requestDto.TrainType, requestDto.WagonType);
            return Json(wagons);
        }
    }
}