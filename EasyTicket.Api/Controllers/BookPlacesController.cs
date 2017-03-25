using System.Threading.Tasks;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;

namespace EasyTicket.Api.Controllers
{
    public class BookPlacesController : BaseController {
        private readonly UzClient UZ;

        public BookPlacesController() {
            UZ = new UzClient();
        }

        // POST api/wagons
        public async Task<IHttpActionResult> Post([FromBody]BookPlacesDto bookPlacesDto) {
            UzContext context = await UZ.GetUZContextAsync();
            BookPlacesResponse places = await UZ.BookPlaceAsync(context, bookPlacesDto.StationFromId, bookPlacesDto.StationToId, bookPlacesDto.DateTime, bookPlacesDto.TrainNumber, bookPlacesDto.WagonNumber, bookPlacesDto.CoachClass, bookPlacesDto.WagonTypeCode, bookPlacesDto.Places[0], bookPlacesDto.PlaceType, "Serhii", "Kharchenko");
            return Json(places);
        }
    }
}