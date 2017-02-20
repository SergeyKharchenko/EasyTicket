﻿using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;

namespace EasyTicket.Api.Controllers {
    public class PlacesController : BaseController {
        private readonly UzClient UZ;

        public PlacesController() {
            UZ = new UzClient();
        }

        // POST api/wagons
        public async Task<IHttpActionResult> Post([FromBody]PlacesRequestDto requestDto) {
            UzContext context = await UZ.GetUZContext();
            PlacesResponse places = await UZ.GetPlaces(context, requestDto.StationFromId, requestDto.StationToId, requestDto.DateTime, requestDto.TrainNumber, requestDto.WagonNumber, requestDto.CoachClass, requestDto.CoachType);
            return Json(places);
        }
    }
}