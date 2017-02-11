using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.SharedResources;

namespace EasyTicket.Api.Controllers {
    public class WagonsController : BaseController {
        private readonly UzClient UZ;

        public WagonsController() {
            UZ = new UzClient();
        }

        // POST api/wagons
        public async Task<HttpResponseMessage> Post([FromBody]WagonsRequest request) {
            UzContext context = await UZ.GetUZContext();
            string wagons = await UZ.GetWagons(context, request.StationIdFrom, request.StationIdTo, request.DateTime,
                                               request.TrainId, request.TrainType, request.WagonType);
            return Json(wagons);
        }
    }

    public class WagonsRequest {
        public int StationIdFrom { get; set; }
        public int StationIdTo { get; set; }
        public string Date { get; set; }
        public string TrainId { get; set; }
        public int TrainType { get; set; }
        public string WagonType { get; set; }

        public DateTime DateTime {
            get { return DateTime.ParseExact(Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
            set { Date = value.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}