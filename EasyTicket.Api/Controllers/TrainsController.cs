using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using EasyTicket.SharedResources;

namespace EasyTicket.Api.Controllers {
    public class TrainsController : BaseController {
        private readonly UzClient UZ;

        public TrainsController() {
            UZ = new UzClient();
        }

        // POST api/trains
        public async Task<HttpResponseMessage> Post([FromBody]TrainsRequest request) {
            UzContext context = await UZ.GetUZContext();
            string trains = await UZ.GetTrains(context, request.StationIdFrom, request.StationIdTo, request.DateTime);
            return Json(trains);
        }
    }

    [ModelBinder(typeof(CustomModelBinder))]
    public class TrainsRequest {
        public int StationIdFrom { get; set; }
        public int StationIdTo { get; set; }
        public string Date { get; set; }
        public DateTime DateTime {
            get { return DateTime.ParseExact(Date, "dd.MM.yyyy", CultureInfo.InvariantCulture); }
            set { Date = value.ToString("dd.MM.yyyy"); }
        }
    }
}
