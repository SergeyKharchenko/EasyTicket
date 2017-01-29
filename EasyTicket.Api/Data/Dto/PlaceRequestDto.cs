using System;
using EasyTicket.Api.Infrastructure.Enums;

namespace EasyTicket.Api.Data.Dto {
    public class PlaceRequestDto {
        public string PassangerName { get; set; }
        public string PassangerEmail { get; set; }
        public WagonType WagonType { get; set; } = WagonType.Coupe;
        public WagonArea WagonArea { get; set; } = WagonArea.Any;
        public PlaceType PlaceType { get; set; } = PlaceType.LeftBottom;
        public string Date { get; set; }
        public int[] Places { get; set; }
    }
}