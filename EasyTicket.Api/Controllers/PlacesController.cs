using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.SharedResources;

namespace EasyTicket.Api.Controllers {
    public class PlacesController : BaseController {
        private readonly UzClient UZ;

        public PlacesController() {
            UZ = new UzClient();
        }

        // POST api/wagons
        public async Task<HttpResponseMessage> Post([FromBody]PlacesRequest request) {
            UzContext context = await UZ.GetUZContext();
            string wagons = await UZ.GetPlaces(context, request.StationIdFrom, request.StationIdTo, request.DateTime, request.TrainId, request.WagonNumber, request.CoachClass, request.CoachType);
            return Json(wagons);
        }
    }

    public class PlacesRequest {
        public int StationIdFrom { get; set; }
        public int StationIdTo { get; set; }
        public string Date { get; set; }
        public string TrainId { get; set; }
        public int WagonNumber { get; set; }    
        public string CoachClass { get; set; }
        public int CoachType { get; set; }

        public DateTime DateTime {
            get { return DateTime.ParseExact(Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
            set { Date = value.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}