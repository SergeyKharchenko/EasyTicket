﻿using System;
using EasyTicket.SharedResources.Models.Responses;
using Newtonsoft.Json.Linq;

namespace EasyTicket.SharedResources.Infrastructure {
    public class ResponseFormatter {
        public StationsResonse FormatStations(string rawStations) {
            JArray rawStationsResponse = JArray.Parse(rawStations);
            var stations = new JArray();
            foreach (JToken jToken in rawStationsResponse) {
                JObject station = JObject.FromObject(new {
                    id = jToken["value"],
                    title = jToken["label"]
                });
                stations.Add(station);
            }
            var stationsResponse = new JObject {
                {
                    "stations", stations
                }
            };
            return BasicJsonSerializer.Deserialize<StationsResonse>(stationsResponse.ToString());
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
                        typeDescription = jWagonsToken["title"],
                        typeCode = jWagonsToken["letter"],
                        freePlaces = jWagonsToken["places"]
                    });
                    wagons.Add(wagon);
                }

                JObject train = JObject.FromObject(new {
                    trainNumber = jTrainToken["num"],
                    travelTime = jTrainToken["travel_time"],
                    trainType = jTrainToken["model"],
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
                date = jStationToken["src_date"]
            });
        }

        public string FormatWagons(string rawWagons) {
            JObject jRawWagons = JObject.Parse(rawWagons);
            var wagons = new JArray();

            foreach (JToken jWagon in jRawWagons["coaches"]) {
                var firstPrice = (JProperty)jWagon["prices"].First;
                decimal price = ((decimal) Convert.ChangeType(((JValue) firstPrice.Value).Value, typeof(decimal)) / 100);
                JObject wagon = JObject.FromObject(new {
                    number = jWagon["num"],
                    typeCode = jWagon["type"],
                    freePlaces = jWagon["places_cnt"],
                    coachType = jWagon["coach_type_id"],
                    coachClass = jWagon["coach_class"],
                    price
                });
                wagons.Add(wagon);
            }
            return new JObject {
                {
                    "wagons", wagons
                }
            }.ToString();
        }

        public string FormatPlaces(string rawPlaces) {
            JObject jRawPlaces = JObject.Parse(rawPlaces);
            return new JObject {
                {
                    "places", jRawPlaces.First.First.First.First.First.First.DeepClone()
                }
            }.ToString();
        }
    }
}