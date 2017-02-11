using System;
using System.Globalization;

namespace EasyTicket.Api.Dto {
    public class WagonsRequestDto {
        public int StationIdFrom { get; set; }
        public int StationIdTo { get; set; }
        public string Date { get; set; }
        public string TrainId { get; set; }
        public int TrainType { get; set; }
        public string WagonType { get; set; }

        public DateTime DateTime {
            get { return DateTime.ParseExact(Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
            set { Date = value.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}