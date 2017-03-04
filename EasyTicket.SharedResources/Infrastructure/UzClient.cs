using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using EasyTicket.SharedResources.Models.Responses;

namespace EasyTicket.SharedResources.Infrastructure {
    public class UzClient {
        private const string BaseUrl = @"http://booking.uz.gov.ua/ru/";
        private const string UrlStation = BaseUrl + @"purchase/station/";
        private const string UrlTrains = BaseUrl + @"purchase/search/";
        private const string UrlWagons = BaseUrl + @"purchase/coaches/";
        private const string UrlPlaces = BaseUrl + @"purchase/coach/";

        private readonly TokenDecoder TokenDecoder = new TokenDecoder();
        private readonly ResponseFormatter ResponseFormatter = new ResponseFormatter();

        public async Task<UzContext> GetUZContext() {
            using (CookieSupportatbleHttpClient client = CreateHttpClient()) {
                HttpResponseMessage response = await client.GetAsync(BaseUrl);
                string mainPageHtml = await response.Content.ReadAsStringAsync();
                string token = TokenDecoder.Decode(mainPageHtml);
                CookieCollection cookies = client.ReadCookies(response);
                return new UzContext(token, cookies);
            }
        }

        public async Task<StationsResonse> GetStations(UzContext context, string term) {
            string normalizedTerm = HttpUtility.UrlEncode(HttpUtility.UrlDecode(term));
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                HttpResponseMessage response = await client.GetAsync(UrlStation + $"?term={normalizedTerm}");
                string rawStations = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatStations(rawStations);
            }
        }

        public async Task<TrainsResponse> GetTrains(UzContext context, int stationFromId, int stationToId, DateTime date) {
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                        {"station_id_from", stationFromId.ToString()},
                        {"station_id_till", stationToId.ToString()},
                        {"date_dep", date.ToString("dd.MM.yyyy")},
                        {"time_dep", "00:00"},
                        {"time_dep_till", ""},
                        {"another_ec", "0"},
                        {"search", ""}
                    });

                HttpResponseMessage response = await client.PostAsync(UrlTrains, content);
                string rawTrains = await response.Content.ReadAsStringAsync();
                string formattedTrains = ResponseFormatter.FormatTrains(rawTrains);
                return BasicJsonSerializer.Deserialize<TrainsResponse>(formattedTrains);
            }
        }

        public async Task<WagonsResponse> GetWagons(UzContext context, int stationFromId, int stationToId, DateTime date, string trainNumber, int trainType, string wagonType) {
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                        {"station_id_from", stationFromId.ToString()},
                        {"station_id_till", stationToId.ToString()},
                        {"date_dep", date.ToUnixTimestamp().ToString(CultureInfo.InvariantCulture)},
                        {"train", trainNumber },
                        {"model", trainType.ToString()},
                        {"coach_type", wagonType},
                        {"round_trip", "0"},
                        {"another_ec", "0"}
                    });

                HttpResponseMessage response = client.PostAsync(UrlWagons, content).Result;
                string rawWagons = await response.Content.ReadAsStringAsync();
                string formattedWagons = ResponseFormatter.FormatWagons(rawWagons);
                return BasicJsonSerializer.Deserialize<WagonsResponse>(formattedWagons);
            }
        }

        public async Task<PlacesResponse> GetPlaces(UzContext context, int stationFromId, int stationToId, DateTime date, string trainNumber, int wagonNumber, string coachClass, int coachType) {
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                        {"station_id_from", stationFromId.ToString()},
                        {"station_id_till", stationToId.ToString()},
                        {"date_dep", date.ToUnixTimestamp().ToString(CultureInfo.InvariantCulture)},
                        {"train", trainNumber },
                        {"coach_num", wagonNumber.ToString()},
                        {"coach_class", coachClass},
                        {"coach_type_id", coachType.ToString()},
                        {"change_scheme", "1"}
                    });

                HttpResponseMessage response = client.PostAsync(UrlPlaces, content).Result;
                string rawPlaces = await response.Content.ReadAsStringAsync();
                string formattedPlaces = ResponseFormatter.FormatPlaces(rawPlaces);
                return BasicJsonSerializer.Deserialize<PlacesResponse>(formattedPlaces);
            }
        }

        private CookieSupportatbleHttpClient CreateHttpClient(UzContext context = null) {
            var httpClient = new CookieSupportatbleHttpClient();

            if (context != null) {
                httpClient.DefaultRequestHeaders.Add("GV-Ajax", "1");
                httpClient.DefaultRequestHeaders.Add("GV-Referer", BaseUrl);
                httpClient.DefaultRequestHeaders.Add("GV-Token", context.Token);
                httpClient.SetCookie(context.Cookie);
            }

            return httpClient;
        }
    }
}