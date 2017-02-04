using System;
using EasyTicket.Api.Infrastructure.Enums;

namespace EasyTicket.Api.Data.Dto {
    public class PlaceRequestDto {
        public string PassangerName { get; set; }
        public string PassangerSurname { get; set; }
        public string PassangerEmail { get; set; }
        public WagonType WagonType { get; set; } = WagonType.Coupe;
        public string Date { get; set; }
        public int[] Places { get; set; }
    }
}