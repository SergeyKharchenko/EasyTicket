using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources;

namespace EasyTicket.Api.Controllers {
    public class StationsController : BaseController {
        private readonly UzClient UZ;

        public StationsController() {
            UZ = new UzClient();
        }

        // POST api/stations
        public async Task<HttpResponseMessage> Post([FromBody] StationsRequestDto requestDto) {
            UzContext context = await UZ.GetUZContext();
            string stations = await UZ.GetStations(context, requestDto.Term);
            return Json(stations);
        }
    }
}
