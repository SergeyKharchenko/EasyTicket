using System;
using System.Collections.Generic;
using System.Linq;
using EasyTicket.SharedResources.Enums;
using EasyTicket.SharedResources.Models.Responses;

namespace ProcessRequestJob {
    public class TicketCheckResult {
        public ICollection<Train> Trains { get; set; }

        public RequestState RequestState { get; set; }

        public override string ToString() {
            return $"For places [{String.Join(", ", Trains.First().Wagons.First().Places)}] there are next {nameof(Trains)}:{Environment.NewLine}" +
                   $"\t\t{String.Join(Environment.NewLine + Environment.NewLine + "\t\t", Trains)}" +
                   $"{Environment.NewLine}{Environment.NewLine}";
        }

        public class Train {
            public string TrainNumber { get; set; }
            public string TravelTime { get; set; }
            public TrainsResponse.Station StationFrom { get; set; }
            public TrainsResponse.Station StationTo { get; set; }
            public ICollection<Wagon> Wagons { get; set; }

            public override string ToString() {
                return
                    $"{nameof(TrainNumber)}: {TrainNumber}{Environment.NewLine}\t\t" +
                    $"{nameof(TravelTime)}: {TravelTime}{Environment.NewLine}\t\t" +
                    $"{nameof(StationFrom)}: {StationFrom}{Environment.NewLine}\t\t" +
                    $"{nameof(StationTo)}: {StationTo}{Environment.NewLine}\t\t" +
                    $"{nameof(Wagons)}:{Environment.NewLine}\t\t\t{string.Join($"{Environment.NewLine}\t\t\t", Wagons)}";
            }
        }

        public class Wagon {
            public string TypeDescription { get; set; }
            public int Number { get; set; }
            public decimal Price { get; set; }
            public string PlaceType { get; set; }
            public string CoachClass { get; set; }
            public string TypeCode { get; set; }
            public int[] Places { get; set; }

            public override string ToString() {
                return $"{nameof(TypeDescription)}: {TypeDescription}, {nameof(Number)}: {Number}, {nameof(Price)}: {Price}";
            }
        }
    }
}