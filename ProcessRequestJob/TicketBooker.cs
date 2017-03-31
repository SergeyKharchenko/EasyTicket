using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;
using static ProcessRequestJob.TicketCheckResult;
using EasyTicket.SharedResources.Models.Tables;

namespace ProcessRequestJob {
    public class TicketBooker {
        public async Task<BookPlacesResponse> BookPlacesAsync(UzContext uzContext, TicketCheckResult checkResult, Request request) {
            var uzClient = new UzClient();

            Tuple<Train, Wagon> cheapestTrainAndWagon = GetCheapestTrainAndWagon(checkResult);
            Train train = cheapestTrainAndWagon.Item1;
            Wagon wagon = cheapestTrainAndWagon.Item2;

            BookPlacesResponse bookingResult =
                await uzClient.BookPlaceAsync(uzContext, train.StationFrom.Id, train.StationTo.Id,
                                              train.StationFrom.DateTime,
                                              train.TrainNumber, wagon.Number, wagon.CoachClass, wagon.TypeCode,
                                              wagon.Places.First(), wagon.PlaceType, request.PassangerName,
                                              request.PassangerSurname);

            return bookingResult;
        }

        private Tuple<Train, Wagon> GetCheapestTrainAndWagon(TicketCheckResult checkResult) {
            IEnumerable<Tuple<Train, Wagon>> allTrainWagonPairs =
                from train in checkResult.Trains
                from wagon in train.Wagons
                select new Tuple<Train, Wagon>(train, wagon);

            return allTrainWagonPairs.OrderBy(pair => pair.Item2.Price).First();
        }
    }
}