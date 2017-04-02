namespace EasyTicket.Api.Dto {
    public class ReservationDto {
        public RequestDto Request { get; set; }
        public string Token { get; set; }
        public long BookingTimestamp { get; set; }
        public string SessionId { get; set; }
    }
}