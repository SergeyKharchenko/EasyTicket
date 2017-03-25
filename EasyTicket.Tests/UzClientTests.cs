using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;
using NUnit.Framework;

namespace EasyTicket.Tests {
    [TestFixture]
    public class UzClientTests {
        private const int LvovStatinId = 2218000;
        private const int KievStationId = 2200001;
        private const int DaysOffset = 10;

        private UzClient _uzClient;

        [SetUp]
        public void SetUp() {
            _uzClient = new UzClient();
        }

        [Test]
        public void GetUZContextAsyncExpectTokenAndCookiesPresent() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            
            Assert.IsFalse(string.IsNullOrWhiteSpace(context.Token));
            Assert.IsNotNull(context.Cookie["_gv_sessid"]);
        }

        [Test]
        public void GetStationsAsyncWithValidTermExpectStationsPresent() {
            UzContext context = _uzClient.GetUZContextAsync().Result;

            StationsResonse stationsResonse = _uzClient.GetStationsAsync(context, "Киев").Result;
            
            CollectionAssert.AreEquivalent(new List<StationsResonse.Station> {
                new StationsResonse.Station {Id = KievStationId, Title = "Киев"},
                new StationsResonse.Station {Id = 2201180, Title = "Киевская Русановка"},
                new StationsResonse.Station {Id = 2058041, Title = "Киевская"},
                new StationsResonse.Station {Id = 2064627, Title = "Киевский"},
            }, stationsResonse.Stations);
        }

        [Test]
        public void GetStationsAsyncWithInvalidTermExpectNoStations() {
            UzContext context = _uzClient.GetUZContextAsync().Result;

            StationsResonse stationsResonse = _uzClient.GetStationsAsync(context, "Зажопинск").Result;
            
            CollectionAssert.IsEmpty(stationsResonse.Stations);
        }

        [Test]
        public void GetTrainsAsyncWithInvalidContextExpectNoTrains() {
            var context = new UzContext("token", new CookieCollection());

            TrainsResponse trainsResponse = _uzClient.GetTrainsAsync(context, 
                KievStationId, // Киев
                LvovStatinId, // Львов
                DateTime.Today.AddDays(DaysOffset)
                ).Result;
            
            CollectionAssert.IsEmpty(trainsResponse.Trains);
        }

        [Test]
        public void GetTrainsAsyncWithValidParametersExpectTrains() {
            UzContext context = _uzClient.GetUZContextAsync().Result;

            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            
            CollectionAssert.IsNotEmpty(trainsResponse.Trains);
        }

        [Test]
        public void GetTrainsAsyncWithInvalidDateExpectNoTrains() {
            UzContext context = _uzClient.GetUZContextAsync().Result;

            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(-1));
            
            CollectionAssert.IsEmpty(trainsResponse.Trains);
        }

        [Test]
        public void GetTrainsAsyncWithInvalidStatinIdExpectNoTrains() {
            UzContext context = _uzClient.GetUZContextAsync().Result;

            TrainsResponse trainsResponse = GetTrains(context, 42, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            
            CollectionAssert.IsEmpty(trainsResponse.Trains);
        }

        [Test]
        public void GetWagonsAsyncWithValidParametersExpectWagons() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();

            WagonsResponse wagonsResponse = GetWagons(context,
                                                      KievStationId,
                                                      LvovStatinId,
                                                      train.StationFrom.DateTime,
                                                      train.TrainNumber,
                                                      train.TrainType,
                                                      train.Wagons.First().TypeCode);
            
            CollectionAssert.IsNotEmpty(wagonsResponse.Wagons);
        }

        [Test]
        public void GetWagonsAsyncWithInvalidStationIdExpectNoWagons() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();

            WagonsResponse wagonsResponse = GetWagons(context,
                                                      KievStationId,
                                                      42,
                                                      train.StationFrom.DateTime,
                                                      train.TrainNumber,
                                                      train.TrainType,
                                                      train.Wagons.First().TypeCode);
            
            CollectionAssert.IsEmpty(wagonsResponse.Wagons);
        }

        [Test]
        public void GetWagonsAsyncWithInvalidTrainNumberExpectNoWagons() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();

            WagonsResponse wagonsResponse = GetWagons(context,
                                                      KievStationId,
                                                      LvovStatinId,
                                                      train.StationFrom.DateTime,
                                                      "invalid_train",
                                                      train.TrainType,
                                                      train.Wagons.First().TypeCode);
            
            CollectionAssert.IsEmpty(wagonsResponse.Wagons);
        }

        [Test]
        public void GetPlacesAsyncWithValidParametersExpectPlaces() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();
            WagonsResponse wagonsResponse = _uzClient.GetWagonsAsync(context, KievStationId, LvovStatinId, train.StationFrom.DateTime, train.TrainNumber, train.TrainType, train.Wagons.First().TypeCode).Result;
            WagonsResponse.Wagon wagon = wagonsResponse.Wagons.First();

            PlacesResponse placesResponse = GetPlaces(context,
                                                      KievStationId,
                                                      LvovStatinId,
                                                      train.StationFrom.DateTime,
                                                      train.TrainNumber,
                                                      wagon.Number,
                                                      wagon.CoachClass,
                                                      wagon.CoachType);
            
            Assert.IsFalse(string.IsNullOrWhiteSpace(placesResponse.PlaceType));
            CollectionAssert.IsNotEmpty(placesResponse.Places);
        }

        [Test]
        public void GetPlacesAsyncWithInvalidStatinIdExpectNoPlaces() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();
            WagonsResponse wagonsResponse = _uzClient.GetWagonsAsync(context, KievStationId, LvovStatinId, train.StationFrom.DateTime, train.TrainNumber, train.TrainType, train.Wagons.First().TypeCode).Result;
            WagonsResponse.Wagon wagon = wagonsResponse.Wagons.First();

            PlacesResponse placesResponse = GetPlaces(context,
                                                      KievStationId,
                                                      42,
                                                      train.StationFrom.DateTime,
                                                      train.TrainNumber,
                                                      wagon.Number,
                                                      wagon.CoachClass,
                                                      wagon.CoachType);
            
            CollectionAssert.IsEmpty(placesResponse.Places);
        }

        [Test]
        public void GetPlacesAsyncWithInvalidDateExpectNoPlaces() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();
            WagonsResponse wagonsResponse = _uzClient.GetWagonsAsync(context, KievStationId, LvovStatinId, train.StationFrom.DateTime, train.TrainNumber, train.TrainType, train.Wagons.First().TypeCode).Result;
            WagonsResponse.Wagon wagon = wagonsResponse.Wagons.First();

            PlacesResponse placesResponse = GetPlaces(context,
                                                      KievStationId,
                                                      LvovStatinId,
                                                      DateTime.Now.AddDays(-1),
                                                      train.TrainNumber,
                                                      wagon.Number,
                                                      wagon.CoachClass,
                                                      wagon.CoachType);
            
            CollectionAssert.IsEmpty(placesResponse.Places);
        }

        [Test]
        public void GetPlacesAsyncWithInvalidCoachTypeExpectNoPlaces() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();
            WagonsResponse wagonsResponse = _uzClient.GetWagonsAsync(context, KievStationId, LvovStatinId, train.StationFrom.DateTime, train.TrainNumber, train.TrainType, train.Wagons.First().TypeCode).Result;
            WagonsResponse.Wagon wagon = wagonsResponse.Wagons.First();

            PlacesResponse placesResponse = GetPlaces(context,
                                                      KievStationId,
                                                      LvovStatinId,
                                                      DateTime.Now.AddDays(-1),
                                                      train.TrainNumber,
                                                      wagon.Number,
                                                      wagon.CoachClass,
                                                      int.MaxValue);
            
            CollectionAssert.IsEmpty(placesResponse.Places);
        }

        [Test]
        public void BookPlaceAsyncWithValidParametersExpectPlaceBooked() {
            UzContext context = _uzClient.GetUZContextAsync().Result;
            TrainsResponse trainsResponse = GetTrains(context, KievStationId, LvovStatinId, DateTime.Today.AddDays(DaysOffset));
            TrainsResponse.Train train = trainsResponse.Trains.First();
            WagonsResponse wagonsResponse = _uzClient.GetWagonsAsync(context, KievStationId, LvovStatinId, train.StationFrom.DateTime, train.TrainNumber, train.TrainType, train.Wagons.First().TypeCode).Result;
            WagonsResponse.Wagon wagon = wagonsResponse.Wagons.First();
            PlacesResponse placesResponse = GetPlaces(context, KievStationId, LvovStatinId, train.StationFrom.DateTime, train.TrainNumber, wagon.Number, wagon.CoachClass, wagon.CoachType);

            BookPlacesResponse bookPlaceResult = BookPlace(context,
                KievStationId,
                LvovStatinId,
                DateTime.Today.AddDays(DaysOffset),
                train.TrainNumber,
                 wagon.Number,
                 wagon.CoachClass,
                 wagon.TypeCode,
                 placesResponse.Places.First(),
                 placesResponse.PlaceType,
                 "Валера",
                 "Топор");

            Assert.IsFalse(bookPlaceResult.IsError);
        }

        private TrainsResponse GetTrains(UzContext context, int stationIdFrom, int stationIdTo, DateTime dateTime) {
            return _uzClient.GetTrainsAsync(context, stationIdFrom, stationIdTo, dateTime).Result;
        }

        private WagonsResponse GetWagons(UzContext context, int stationIdFrom, int stationIdTo, DateTime dateTime, string trainNumber, int trainType, string wagonType) {
            return _uzClient.GetWagonsAsync(context, stationIdFrom, stationIdTo, dateTime, trainNumber, trainType, wagonType).Result;
        }

        private PlacesResponse GetPlaces(UzContext context, int stationIdFrom, int stationIdTo, DateTime dateTime, string trainNumber, int wagonNumber, string coachClass, int coachType) {
            return _uzClient.GetPlacesAsync(context, stationIdFrom, stationIdTo, dateTime, trainNumber, wagonNumber, coachClass, coachType).Result;
        }

        private BookPlacesResponse BookPlace(UzContext context, int stationFromId, int stationToId, DateTime dateTime, string trainNumber, int wagonNumber, string coachClass, string wagonTypeCode, int place, string placeType, string firstName, string lastName) {
            return _uzClient.BookPlaceAsync(context, stationFromId, stationToId, dateTime, trainNumber, wagonNumber, coachClass, wagonTypeCode, place, placeType, firstName, lastName).Result;
        }
    }
}