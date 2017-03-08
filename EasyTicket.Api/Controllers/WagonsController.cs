using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;

namespace EasyTicket.Api.Controllers {
    public class WagonsController : BaseController {
        private readonly UzClient UZ;

        public WagonsController() {
            UZ = new UzClient();
        }

        // POST api/wagons
        public async Task<IHttpActionResult> Post([FromBody]WagonsRequestDto requestDto) {
            UzContext context = await UZ.GetUZContextAsync();
            WagonsResponse wagons = await UZ.GetWagonsAsync(context, requestDto.StationFromId, requestDto.StationToId, requestDto.DateTime,
                                               requestDto.TrainNumber, requestDto.TrainType, requestDto.WagonType);
            return Json(wagons);
        }
    }
}