using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Tables;
using EasyTicket.SharedResources.Models.Responses;

namespace ProcessRequestJob {
    public class TicketChecker {
        private readonly UzClient _uzClient;

        public TicketChecker() {
            _uzClient = new UzClient();
        }

        public async void Check() {
            UzContext uzContext = await _uzClient.GetUZContext();
            foreach (Request request in GetRequests()) {
                CheckResult checkResult = await CheckTicket(uzContext, request);
                if (checkResult.Found) {
                    Console.WriteLine(checkResult);
                }
            }
        }

        private Request[] GetRequests() {
            using (var uzDbContext = new UzDbContext()) {
                return uzDbContext.Requests.ToArray();
            }
        }

        private async Task<CheckResult> CheckTicket(UzContext uzContext, Request request) {
            var resultedTrains = new List<Train>();

            string wagonTypeCode = UzFormatConverter.Get(request.WagonType);
            ICollection<TrainsResponse.Train> trains = await GetTrains(uzContext, request, wagonTypeCode);
            foreach (TrainsResponse.Train train in trains) {
                ICollection<WagonsResponse.Wagon> wagons = await GetWagons(uzContext, train, wagonTypeCode, request.Places);
                if (!wagons.Any()) {
                    continue;
                }
                Train resultedTrain = ConvertToResultedTrain(train, wagons, wagonTypeCode);
                resultedTrains.Add(resultedTrain);
            }
            return new CheckResult {
                Trains = resultedTrains,
                Places = request.Places
            };
        }

        private async Task<ICollection<TrainsResponse.Train>> GetTrains(UzContext uzContext, Request request, string wagonTypeId) {
            TrainsResponse trainsResponse = await _uzClient.GetTrains(uzContext, request.StationFromId, request.StationToId,
                                                                      request.DateTime);
            return (from train in trainsResponse.Trains
                    from wagon in train.Wagons
                    where wagonTypeId == wagon.TypeCode
                    select train).ToList();
        }

        private async Task<ICollection<WagonsResponse.Wagon>> GetWagons(UzContext uzContext, TrainsResponse.Train train, string wagonTypeId, int[] requestedPlaces) {
            WagonsResponse wagonsResponse =
                await _uzClient.GetWagons(uzContext, train.StationFrom.Id,
                                          train.StationTo.Id,
                                          train.StationFrom.DateTime,
                                          train.TrainNumber, train.TrainType, wagonTypeId);

            return await FilterWagonsByAvailablePlaces(uzContext, train, requestedPlaces, wagonsResponse);
        }

        private async Task<ICollection<WagonsResponse.Wagon>> FilterWagonsByAvailablePlaces(UzContext uzContext, TrainsResponse.Train train, int[] requestedPlaces, WagonsResponse wagonsResponse) {
            List<WagonsResponse.Wagon> wagons = new List<WagonsResponse.Wagon>();
            foreach (WagonsResponse.Wagon wagon in wagonsResponse.Wagons) {
                bool isPlaceAvailable = await IsPlaceAvailable(uzContext, train, wagon, requestedPlaces);
                if (isPlaceAvailable) {
                    wagons.Add(wagon);
                }
            }
            return wagons;
        }

        private async Task<bool> IsPlaceAvailable(UzContext uzContext, TrainsResponse.Train train, WagonsResponse.Wagon wagon, int[] requestedPlaces) {
            PlacesResponse placesResponse =
                await _uzClient.GetPlaces(uzContext, train.StationFrom.Id, train.StationTo.Id,
                                          train.StationFrom.DateTime,
                                          train.TrainNumber, wagon.Number, wagon.CoachClass, wagon.CoachType);
            return !requestedPlaces.Except(placesResponse.Places).Any();
        }

        private Train ConvertToResultedTrain(TrainsResponse.Train train, ICollection<WagonsResponse.Wagon> wagons, string wagonTypeCode) {
            string wagonTypeDescription = train.Wagons.First(wagon => wagon.TypeCode == wagonTypeCode).TypeDescription;

            List<Wagon> resultedWagons = (from wagon in wagons
                                          where wagon.TypeCode == wagonTypeCode
                                          let wagonType = wagonTypeDescription
                                          select new Wagon {
                                              Number = wagon.Number,
                                              Price = wagon.Price,
                                              TypeDescription = wagonTypeDescription
                                          }).ToList();

            return new Train {
                StationFrom = train.StationFrom,
                StationTo = train.StationTo,
                TrainNumber = train.TrainNumber,
                TravelTime = train.TravelTime,
                Wagons = resultedWagons
            };
        }

        private class CheckResult {
            public bool Found => Trains.Any(train => train.Wagons.Any());

            public ICollection<Train> Trains { get; set; }
            public int[] Places { get; set; }

            public override string ToString() {
                return $"For places [{string.Join(", ", Places)}] there are next {nameof(Trains)}:{Environment.NewLine}" +
                       $"\t\t{string.Join(Environment.NewLine + Environment.NewLine + "\t\t", Trains)}" +
                       $"{Environment.NewLine}{Environment.NewLine}";
            }
        }

        private class Train {
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

        private class Wagon {
            public string TypeDescription { get; set; }
            public int Number { get; set; }
            public decimal Price { get; set; }

            public override string ToString() {
                return $"{nameof(TypeDescription)}: {TypeDescription}, {nameof(Number)}: {Number}, {nameof(Price)}: {Price}";
            }
        }
    }
}