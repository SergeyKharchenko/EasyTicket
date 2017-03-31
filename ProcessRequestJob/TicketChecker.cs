using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Enums;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Tables;
using EasyTicket.SharedResources.Models.Responses;
using SendGrid;
using static ProcessRequestJob.TicketCheckResult;

namespace ProcessRequestJob {
    public class TicketChecker {
        private readonly UzClient _uzClient;
        private readonly TicketBooker _ticketBooker;

        public TicketChecker() {
            _uzClient = new UzClient();
            _ticketBooker = new TicketBooker();
        }

        public async void Check() {
            UzContext uzContext = await _uzClient.GetUZContextAsync();
            Request[] requests = GetRequests();
            Console.WriteLine($"Checking requests: {requests.Length}");
            foreach (Request request in requests) {
                TicketCheckResult checkResult = await CheckTicket(uzContext, request);
                switch (checkResult.RequestState) {
                    case RequestState.Expired: {
                        NotifyExpired(request);
                        break;
                    }
                    case RequestState.Found: {
                        BookPlacesResponse bookingResult = await _ticketBooker.BookPlacesAsync(uzContext, checkResult, request);
                        if (!bookingResult.IsError) {
                            NotifyFoundAsync(bookingResult, request);
                        }
                        Console.WriteLine(checkResult);
                        break;
                    }
                }
            }
        }

        private Request[] GetRequests() {
            using (var uzDbContext = new UzDbContext()) {
                return uzDbContext.Requests.Where(request => request.State == RequestState.Requested).ToArray();
            }
        }

        private async Task<TicketCheckResult> CheckTicket(UzContext uzContext, Request request) {
            if (IsRequestExpired(request)) {
                return new TicketCheckResult {
                    RequestState = RequestState.Expired
                };
            }

            var resultedTrains = new List<Train>();

            string wagonTypeCode = UzFormatConverter.Get(request.WagonType);
            ICollection<TrainsResponse.Train> trains = await GetTrains(uzContext, request, wagonTypeCode);
            foreach (TrainsResponse.Train train in trains) {
                ICollection<ResponseWagonWithPlaceInfo> wagons = await GetWagons(uzContext, train, wagonTypeCode, request);
                if (!wagons.Any()) {
                    continue;
                }
                Train resultedTrain = ConvertToResultedTrain(train, wagons, wagonTypeCode);
                resultedTrains.Add(resultedTrain);
            }
            return new TicketCheckResult {
                Trains = resultedTrains,
                RequestState = resultedTrains.Any(train => train.Wagons.Any()) ? RequestState.Found : RequestState.Requested
            };
        }

        private bool IsRequestExpired(Request request) {
            return request.DateTime < DateTime.Today;
        }

        private async Task<ICollection<TrainsResponse.Train>> GetTrains(UzContext uzContext, Request request, string wagonTypeId) {
            TrainsResponse trainsResponse = await _uzClient.GetTrainsAsync(uzContext, request.StationFromId, request.StationToId,
                                                                      request.DateTime);
            return (from train in trainsResponse.Trains
                    from wagon in train.Wagons
                    where wagonTypeId == wagon.TypeCode
                    select train).ToList();
        }

        private async Task<ICollection<ResponseWagonWithPlaceInfo>> GetWagons(UzContext uzContext, TrainsResponse.Train train, string wagonTypeId, Request request) {
            WagonsResponse wagonsResponse =
                await _uzClient.GetWagonsAsync(uzContext, train.StationFrom.Id,
                                          train.StationTo.Id,
                                          train.StationFrom.DateTime,
                                          train.TrainNumber, train.TrainType, wagonTypeId);

            return await FilterWagonsByAvailablePlaces(uzContext, train, wagonsResponse, request);
        }

        private async Task<ICollection<ResponseWagonWithPlaceInfo>> FilterWagonsByAvailablePlaces(UzContext uzContext, TrainsResponse.Train train, WagonsResponse wagonsResponse, Request request) {
            var wagons = new List<ResponseWagonWithPlaceInfo>();
            foreach (WagonsResponse.Wagon wagon in wagonsResponse.Wagons) {
                PlacesResponse placesResponse = await GetPlacesAsync(uzContext, train, wagon, request);
                bool isPlaceAvailable = IsPlaceAvailable(placesResponse, request);
                if (isPlaceAvailable) {
                    ResponseWagonWithPlaceInfo wagonWithPlaceInfo = ExtendResponseWagonWithPlaceInfo(wagon, placesResponse, request);
                    wagons.Add(wagonWithPlaceInfo);
                }
            }
            return wagons;
        }

        private async Task<PlacesResponse> GetPlacesAsync(UzContext uzContext, TrainsResponse.Train train, WagonsResponse.Wagon wagon, Request request) {
            return await _uzClient.GetPlacesAsync(uzContext, train.StationFrom.Id, train.StationTo.Id,
                                          train.StationFrom.DateTime,
                                          train.TrainNumber, wagon.Number, wagon.CoachClass, wagon.CoachType);
        }

        private bool IsPlaceAvailable(PlacesResponse placesResponse, Request request) {
            switch (request.SearchType) {
                case SearchType.Any: {
                        return request.Places.Intersect(placesResponse.Places).Any();
                    }
                default: {
                        return !request.Places.Except(placesResponse.Places).Any();
                    }
            }
        }

        private ResponseWagonWithPlaceInfo ExtendResponseWagonWithPlaceInfo(WagonsResponse.Wagon wagon, PlacesResponse placesResponse, Request request) {
            return new ResponseWagonWithPlaceInfo {
                Number = wagon.Number,
                Price = wagon.Price,
                CoachClass = wagon.CoachClass,
                CoachType = wagon.CoachType,
                FreePlaces = wagon.FreePlaces,
                TypeCode = wagon.TypeCode,
                PlaceType = placesResponse.PlaceType,
                Places = request.Places.Intersect(placesResponse.Places).ToArray()
            };
        }

        private Train ConvertToResultedTrain(TrainsResponse.Train train, ICollection<ResponseWagonWithPlaceInfo> wagons, string wagonTypeCode) {
            string wagonTypeDescription = train.Wagons.First(wagon => wagon.TypeCode == wagonTypeCode).TypeDescription;

            List<Wagon> resultedWagons = (from wagon in wagons
                                          where wagon.TypeCode == wagonTypeCode
                                          let wagonType = wagonTypeDescription
                                          select new Wagon {
                                              Number = wagon.Number,
                                              Price = wagon.Price,
                                              TypeDescription = wagonTypeDescription,
                                              PlaceType = wagon.PlaceType,
                                              Places = wagon.Places,
                                              CoachClass = wagon.CoachClass,
                                              TypeCode = wagon.TypeCode
                                          }).ToList();

            return new Train {
                StationFrom = train.StationFrom,
                StationTo = train.StationTo,
                TrainNumber = train.TrainNumber,
                TravelTime = train.TravelTime,
                Wagons = resultedWagons
            };
        }

        private void NotifyExpired(Request request) {
            request.State = RequestState.Expired;
            using (var uzDbContext = new UzDbContext()) {
                uzDbContext.Entry(request).State = EntityState.Modified;
                uzDbContext.SaveChanges();
            }
        }

        private async void NotifyFoundAsync(BookPlacesResponse bookingResult, Request request) {
            var reservation = new Reservation {
                Token = Guid.NewGuid().ToString(),
                RequestId = request.Id,
                BookingTimestamp = DateTime.Now.Ticks,
                SessionId = bookingResult.Cookies["_gv_sessid"]
            };
            request.State = RequestState.Found;
            using (var uzDbContext = new UzDbContext()) {
                uzDbContext.Reservations.Add(reservation);
                uzDbContext.Entry(request).State = EntityState.Modified;
                uzDbContext.SaveChanges();
            }

            Response mailSendingResponse = await MailSender.Send(request, reservation.Token, reservation.SessionId);
        }

        private class ResponseWagonWithPlaceInfo : WagonsResponse.Wagon {
            public string PlaceType { get; set; }
            public int[] Places { get; set; }
        }
    }
}