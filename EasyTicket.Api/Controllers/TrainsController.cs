using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using EasyTicket.Api.Infrastructure;

namespace EasyTicket.Api.Controllers {
    public class TrainsController : ApiController {
        private readonly UZClient UZ;

        public TrainsController() {
            UZ = new UZClient();
        }

        // POST api/trains
        public async Task<HttpResponseMessage> Post([FromBody]TrainsRequest request) {
            UZContext context = await UZ.GetUZContext();
            string trains = await UZ.GetTrains(context, request.StationIdFrom, request.StationIdTo, request.DateTime);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(trains, Encoding.UTF8, "application/json");
            return response;
        }
    }

    [ModelBinder(typeof(CustomModelBinder))]
    public class TrainsRequest {
        private string vDate;
        public int StationIdFrom { get; set; }
        public int StationIdTo { get; set; }

        public string Date {
            get { return vDate; }
            set { vDate = value; }
        }

        public DateTime DateTime {
            get { return DateTime.Parse(Date); }
            set { Date = value.ToString("dd.MM.yyyy"); }
        }
    }
}
