using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;

namespace EasyTicket.Api.Controllers {
    public class StationsController : BaseController {
        private readonly UzClient UZ;

        public StationsController() {
            UZ = new UzClient();
        }

        // POST api/stations
        public async Task<IHttpActionResult> Post([FromBody] StationsRequestDto requestDto) {
            UzContext context = await UZ.GetUZContext();
            StationsResonse stations = await UZ.GetStations(context, requestDto.Term);
            return Json(stations);
        }
    }
}
