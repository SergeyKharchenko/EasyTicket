using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EasyTicket.Workbench {
    class Program {
        static void Main(string[] args) {
            var res = FormatTrains(@"{
  ""value"": [
    {
      ""num"": ""111О"",
      ""model"": 0,
      ""category"": 0,
      ""travel_time"": ""10:32"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Харьков-Пасс"",
        ""date"": 1485487500,
        ""src_date"": ""2017-01-27 05:25:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Львов"",
        ""date"": 1485525420,
        ""src_date"": ""2017-01-27 15:57:00""
      },
      ""types"": [
        {
          ""title"": ""Люкс"",
          ""letter"": ""Л"",
          ""places"": 18
        },
        {
          ""title"": ""Купе"",
          ""letter"": ""К"",
          ""places"": 8
        }
      ]
    },
    {
      ""num"": ""705К"",
      ""model"": 1,
      ""category"": 1,
      ""travel_time"": ""5:20"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Киев-Пассажирский"",
        ""date"": 1485492300,
        ""src_date"": ""2017-01-27 06:45:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Пшемысль"",
        ""date"": 1485511500,
        ""src_date"": ""2017-01-27 12:05:00""
      },
      ""types"": [
        {
          ""title"": ""Сидячий первого класса"",
          ""letter"": ""С1"",
          ""places"": 99
        },
        {
          ""title"": ""Сидячий второго класса"",
          ""letter"": ""С2"",
          ""places"": 215
        }
      ]
    },
    {
      ""num"": ""747К"",
      ""model"": 3,
      ""category"": 2,
      ""travel_time"": ""7:30"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Дарница"",
        ""date"": 1485492900,
        ""src_date"": ""2017-01-27 06:55:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Львов"",
        ""date"": 1485519900,
        ""src_date"": ""2017-01-27 14:25:00""
      },
      ""types"": [
        {
          ""title"": ""Сидячий первого класса"",
          ""letter"": ""С1"",
          ""places"": 119
        },
        {
          ""title"": ""Сидячий второго класса"",
          ""letter"": ""С2"",
          ""places"": 378
        }
      ]
    },
    {
      ""num"": ""141К"",
      ""model"": 0,
      ""category"": 0,
      ""travel_time"": ""12:56"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Киев-Пассажирский"",
        ""date"": 1485524640,
        ""src_date"": ""2017-01-27 15:44:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Львов"",
        ""date"": 1485571200,
        ""src_date"": ""2017-01-28 04:40:00""
      },
      ""types"": [
        {
          ""title"": ""Люкс"",
          ""letter"": ""Л"",
          ""places"": 14
        },
        {
          ""title"": ""Купе"",
          ""letter"": ""К"",
          ""places"": 51
        },
        {
          ""title"": ""Плацкарт"",
          ""letter"": ""П"",
          ""places"": 17
        }
      ]
    },
    {
      ""num"": ""743П"",
      ""model"": 1,
      ""category"": 1,
      ""travel_time"": ""5:05"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Дарница"",
        ""date"": 1485530280,
        ""src_date"": ""2017-01-27 17:18:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Трускавец"",
        ""date"": 1485548580,
        ""src_date"": ""2017-01-27 22:23:00""
      },
      ""types"": [
        {
          ""title"": ""Сидячий первого класса"",
          ""letter"": ""С1"",
          ""places"": 117
        },
        {
          ""title"": ""Сидячий второго класса"",
          ""letter"": ""С2"",
          ""places"": 154
        }
      ]
    },
    {
      ""num"": ""099К"",
      ""model"": 0,
      ""category"": 0,
      ""travel_time"": ""8:11"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Киев-Пассажирский"",
        ""date"": 1485531720,
        ""src_date"": ""2017-01-27 17:42:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Ужгород"",
        ""date"": 1485561180,
        ""src_date"": ""2017-01-28 01:53:00""
      },
      ""types"": [
        {
          ""title"": ""Купе"",
          ""letter"": ""К"",
          ""places"": 2
        },
        {
          ""title"": ""Плацкарт"",
          ""letter"": ""П"",
          ""places"": 1
        }
      ]
    },
    {
      ""num"": ""049К"",
      ""model"": 0,
      ""category"": 0,
      ""travel_time"": ""8:20"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Киев-Пассажирский"",
        ""date"": 1485540720,
        ""src_date"": ""2017-01-27 20:12:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Трускавец"",
        ""date"": 1485570720,
        ""src_date"": ""2017-01-28 04:32:00""
      },
      ""types"": [
        {
          ""title"": ""Люкс"",
          ""letter"": ""Л"",
          ""places"": 2
        }
      ]
    },
    {
      ""num"": ""149К"",
      ""model"": 0,
      ""category"": 0,
      ""travel_time"": ""6:31"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Киев-Пассажирский"",
        ""date"": 1485548820,
        ""src_date"": ""2017-01-27 22:27:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Ивано-Франковск"",
        ""date"": 1485572280,
        ""src_date"": ""2017-01-28 04:58:00""
      },
      ""types"": [
        {
          ""title"": ""Купе"",
          ""letter"": ""К"",
          ""places"": 19
        }
      ]
    },
    {
      ""num"": ""091К"",
      ""model"": 0,
      ""category"": 0,
      ""travel_time"": ""7:19"",
      ""from"": {
        ""station_id"": 2200001,
        ""station"": ""Киев-Пассажирский"",
        ""date"": 1485549660,
        ""src_date"": ""2017-01-27 22:41:00""
      },
      ""till"": {
        ""station_id"": 2218000,
        ""station"": ""Львов"",
        ""date"": 1485576000,
        ""src_date"": ""2017-01-28 06:00:00""
      },
      ""types"": [
        {
          ""title"": ""Люкс"",
          ""letter"": ""Л"",
          ""places"": 1
        },
        {
          ""title"": ""Купе"",
          ""letter"": ""К"",
          ""places"": 14
        }
      ]
    }
  ],
  ""error"": null,
  ""data"": null,
  ""captcha"": null
}");
            Console.WriteLine(res);
            Console.ReadKey();
        }

        public static string FormatTrains(string rawTrains)
        {
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
            var trainsResponse = new JObject { { "trains", trains } };
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
