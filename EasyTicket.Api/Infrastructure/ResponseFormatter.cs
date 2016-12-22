using Newtonsoft.Json.Linq;

namespace EasyTicket.Api.Infrastructure {
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
            var stationsResponse = new JObject { { "stations", stations } };
            return stationsResponse.ToString();
        }
    }
}