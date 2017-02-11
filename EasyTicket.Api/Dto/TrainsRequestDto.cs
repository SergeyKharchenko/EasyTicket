using System;
using System.Globalization;

namespace EasyTicket.Api.Dto {
    public class TrainsRequestDto {
        public int StationIdFrom { get; set; }
        public int StationIdTo { get; set; }
        public string Date { get; set; }
        public DateTime DateTime {
            get { return DateTime.ParseExact(Date, "dd.MM.yyyy", CultureInfo.InvariantCulture); }
            set { Date = value.ToString("dd.MM.yyyy"); }
        }
    }
}
