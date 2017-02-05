using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.SharedResources;

namespace EasyTicket.Api.Controllers {
    public class StationsController : ApiController {
        private readonly UzClient UZ;

        public StationsController() {
            UZ = new UzClient();
        }

        // POST api/stations
        public async Task<HttpResponseMessage> Post([FromBody] StationsRequest request) {
            UzContext context = await UZ.GetUZContext();
            string stations = await UZ.GetStations(context, request.Term);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(stations, Encoding.UTF8, "application/json");
            return response;
        }
    }

    public class StationsRequest {
        public string Term { get; set; }
    }
}
