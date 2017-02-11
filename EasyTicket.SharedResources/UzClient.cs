using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EasyTicket.SharedResources {
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

        public async Task<string> GetStations(UzContext context, string term) {
            string normalizedTerm = HttpUtility.UrlEncode(HttpUtility.UrlDecode(term));
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                HttpResponseMessage response = await client.GetAsync(UrlStation + normalizedTerm + "/");
                string rawStations = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatStations(rawStations);
            }
        }

        public async Task<string> GetTrains(UzContext context, int stationIdFrom, int stationIdTo, DateTime date) {
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                client.SetCookie(context.Cookie);
                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                        {"station_id_from", stationIdFrom.ToString()},
                        {"station_id_till", stationIdTo.ToString()},
                        {"date_dep", date.ToString("dd.MM.yyyy")},
                        {"time_dep", "00:00"},
                        {"time_dep_till", ""},
                        {"another_ec", "0"},
                        {"search", ""}
                    });

                HttpResponseMessage response = client.PostAsync(UrlTrains, content).Result;
                string rawTrains = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatTrains(rawTrains);
            }
        }

        public async Task<string> GetWagons(UzContext context, int stationIdFrom, int stationIdTo, DateTime date, string trainId, int trainType, string wagonType) {
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                client.SetCookie(context.Cookie);
                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                        {"station_id_from", stationIdFrom.ToString()},
                        {"station_id_till", stationIdTo.ToString()},
                        {"date_dep", date.ToUnixTimestamp().ToString(CultureInfo.InvariantCulture)},
                        {"train", trainId },
                        {"model", trainType.ToString()},
                        {"coach_type", wagonType},
                        {"round_trip", "0"},
                        {"another_ec", "0"}
                    });

                HttpResponseMessage response = client.PostAsync(UrlWagons, content).Result;
                string rawWagons = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatWagons(rawWagons);
            }
        }

        public async Task<string> GetPlaces(UzContext context, int stationIdFrom, int stationIdTo, DateTime date, string trainId, int wagonNumber, string coachClass, int coachType) {
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                client.SetCookie(context.Cookie);
                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                        {"station_id_from", stationIdFrom.ToString()},
                        {"station_id_till", stationIdTo.ToString()},
                        {"date_dep", date.ToUnixTimestamp().ToString(CultureInfo.InvariantCulture)},
                        {"train", trainId },
                        {"coach_num", wagonNumber.ToString()},
                        {"coach_class", coachClass},
                        {"coach_type_id", coachType.ToString()},
                        {"change_scheme", "1"}
                    });

                HttpResponseMessage response = client.PostAsync(UrlPlaces, content).Result;
                string rawPlaces = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatPlaces(rawPlaces);
            }
        }

        private CookieSupportatbleHttpClient CreateHttpClient(UzContext context = null) {
            var httpClient = new CookieSupportatbleHttpClient();

            if (context != null) {
                httpClient.DefaultRequestHeaders.Add("GV-Ajax", "1");
                httpClient.DefaultRequestHeaders.Add("GV-Referer", BaseUrl);
                httpClient.DefaultRequestHeaders.Add("GV-Token", context.Token);
            }

            return httpClient;
        }
    }
}