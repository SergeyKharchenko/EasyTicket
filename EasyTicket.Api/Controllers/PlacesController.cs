using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Infrastructure;

namespace EasyTicket.Api.Controllers {
    public class PlacesController : BaseController {
        private readonly UzClient UZ;

        public PlacesController() {
            UZ = new UzClient();
        }

        // POST api/wagons
        public async Task<HttpResponseMessage> Post([FromBody]PlacesRequestDto requestDto) {
            UzContext context = await UZ.GetUZContext();
            string wagons = await UZ.GetPlaces(context, requestDto.StationFromId, requestDto.StationIdTo, requestDto.DateTime, requestDto.TrainId, requestDto.WagonNumber, requestDto.CoachClass, requestDto.CoachType);
            return Json(wagons);
        }
    }
}