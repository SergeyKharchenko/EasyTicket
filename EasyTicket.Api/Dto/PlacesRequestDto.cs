using System;
using System.Globalization;

namespace EasyTicket.Api.Dto {
    public class PlacesRequestDto {
        public int StationFromId { get; set; }
        public int StationToId { get; set; }
        public string Date { get; set; }
        public string TrainNumber { get; set; }
        public int WagonNumber { get; set; }    
        public string CoachClass { get; set; }
        public int CoachType { get; set; }

        public DateTime DateTime {
            get { return DateTime.ParseExact(Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
            set { Date = value.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}