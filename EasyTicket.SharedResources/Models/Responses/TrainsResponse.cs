using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTicket.SharedResources.Models.Responses {
    public class TrainsResponse {
        public ICollection<Train> Trains { get; set; } = new List<Train>();

        public class Train {
            public string TrainNumber { get; set; }
            public string TravelTime { get; set; }
            public int TrainType { get; set; }
            public Station StationFrom { get; set; }
            public Station StationTo { get; set; }
            public ICollection<Wagon> Wagons { get; set; }
        }

        public class Station {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Date { get; set; }

            public DateTime DateTime {
                get { return DateTime.ParseExact(Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); }
                set { Date = value.ToString("yyyy-MM-dd HH:mm:ss"); }
            }

            public override string ToString() {
                return $"{nameof(Title)}: {Title}, {nameof(Date)}: {Date}";
            }
        }

        public class Wagon {
            public string TypeDescription { get; set; }
            public string TypeCode { get; set; }
            public int FreePlaces { get; set; }
        }
    }
}