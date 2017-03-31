using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace EasyTicket.SharedResources.Models.Tables {
    public class Reservation {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public virtual Request Request { get; set; }
        public int RequestId { get; set; }
        public long BookingTimestamp { get; set; }
        public string SessionId { get; set; }
    }
}