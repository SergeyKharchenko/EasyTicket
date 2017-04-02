using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using EasyTicket.Api.Dto;
using EasyTicket.Api.Infrastructure;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Models.Tables;

namespace EasyTicket.Api.Controllers {
    public class ReservationController : BaseController {
        [HttpGet]
        [Route("api/Reservation/{token}")]
        public async System.Threading.Tasks.Task<IHttpActionResult> RequestPlaceAsync(string token) {
            if (!ModelState.IsValid) {
                return ValidationError();
            }
            Reservation reservation;
            using (var context = new UzDbContext()) {
                reservation = await context.Reservations.Where(reserv => reserv.Token == token)
                                           .Include(reserv => reserv.Request)
                                           .FirstOrDefaultAsync();
            }
            if (reservation == null) {
                return NotFound();
            }
            ReservationDto reservationDto = Mapper.Map<Reservation, ReservationDto>(reservation);
            return Json(reservationDto);
        }
    }
}