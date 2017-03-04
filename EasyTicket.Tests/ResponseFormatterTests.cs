using System;
using System.Collections.Generic;
using EasyTicket.SharedResources.Infrastructure;
using EasyTicket.SharedResources.Models.Responses;
using NUnit.Framework;

namespace EasyTicket.Tests {
    [TestFixture]
    public class ResponseFormatterTests {
        private ResponseFormatter _responseFormatter;

        [SetUp]
        public void SetUp() {
            _responseFormatter = new ResponseFormatter();
        }

        [Test]
        public void FormatStationsTestWithValidData() {
            string rawResponse =
                "[{\"label\":\"Киев\",\"value\":\"2200001\"}," +
                "{\"label\":\"Киевская Русановка\",\"value\":\"2201180\"}," +
                "{\"label\":\"Киевская\",\"value\":\"2058041\"}," +
                "{\"label\":\"Киевский\",\"value\":\"2064627\"}]";

            StationsResonse stations = _responseFormatter.FormatStations(rawResponse);

            CollectionAssert.AreEquivalent(new List<StationsResonse.Station> {
                new StationsResonse.Station {Id = 2200001, Title = "Киев"},
                new StationsResonse.Station {Id = 2201180, Title = "Киевская Русановка"},
                new StationsResonse.Station {Id = 2058041, Title = "Киевская"},
                new StationsResonse.Station {Id = 2064627, Title = "Киевский"},
            }, stations.Stations);
        }
    }
}