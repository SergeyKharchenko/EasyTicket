using Newtonsoft.Json.Linq;

namespace EasyTicket.SharedResources {
    public class ResponseFormatter {
        public string FormatStations(string rawStations) {
            JObject rawStationsResponse = JObject.Parse(rawStations);
            var stations = new JArray();
            foreach (JToken jToken in rawStationsResponse.First.First) {
                JObject station = JObject.FromObject(new {
                    id = jToken["station_id"],
                    title = jToken["title"]
                });
                stations.Add(station);
            }
            var stationsResponse = new JObject {
                {
                    "stations", stations
                }
            };
            return stationsResponse.ToString();
        }

        public string FormatTrains(string rawTrains) {
            JObject rawTrainsResponse = JObject.Parse(rawTrains);
            var trains = new JArray();
            foreach (JToken jTrainToken in rawTrainsResponse.First.First) {
                JObject stationFrom = FormatTrainStation(jTrainToken["from"]);
                JToken stationTo = FormatTrainStation(jTrainToken["till"]);

                var wagons = new JArray();
                foreach (JToken jWagonsToken in jTrainToken["types"]) {
                    JObject wagon = JObject.FromObject(new {
                        type = jWagonsToken["title"],
                        typeCode = jWagonsToken["letter"],
                        freePlaces = jWagonsToken["places"]
                    });
                    wagons.Add(wagon);
                }

                JObject train = JObject.FromObject(new {
                    trainNumber = jTrainToken["num"],
                    travelTime = jTrainToken["travel_time"],
                    model = jTrainToken["model"],
                    stationFrom,
                    stationTo,
                    wagons
                });
                trains.Add(train);
            }
            var trainsResponse = new JObject {
                {
                    "trains", trains
                }
            };
            return trainsResponse.ToString();
        }

        private static JObject FormatTrainStation(JToken jStationToken) {
            return JObject.FromObject(new {
                id = jStationToken["station_id"],
                title = jStationToken["station"],
                dateCode = jStationToken["date"],
                date = jStationToken["src_date"]
            });
        }
    }
}