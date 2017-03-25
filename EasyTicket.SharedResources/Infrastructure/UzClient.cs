using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using EasyTicket.SharedResources.Models.Responses;
using System.Linq;

namespace EasyTicket.SharedResources.Infrastructure {
    public class UzClient {
        private const string BaseUrl = @"http://booking.uz.gov.ua/ru/";
        private const string UrlStation = BaseUrl + @"purchase/station/";
        private const string UrlTrains = BaseUrl + @"purchase/search/";
        private const string UrlWagons = BaseUrl + @"purchase/coaches/";
        private const string UrlPlaces = BaseUrl + @"purchase/coach/";
        private const string UrlBook = BaseUrl + @"cart/add/";

        private const string UzCookiePrefix = "_gv";

        private readonly TokenDecoder TokenDecoder = new TokenDecoder();
        private readonly ResponseFormatter ResponseFormatter = new ResponseFormatter();

        public async Task<UzContext> GetUZContextAsync() {
            using (CookieSupportatbleHttpClient client = CreateHttpClient()) {
                HttpResponseMessage response = await client.GetAsync(BaseUrl);
                string mainPageHtml = await response.Content.ReadAsStringAsync();
                string token = TokenDecoder.Decode(mainPageHtml);
                CookieCollection cookies = client.ReadCookies(response);
                return new UzContext(token, cookies);
            }
        }

        public async Task<StationsResonse> GetStationsAsync(UzContext context, string term) {
            string normalizedTerm = HttpUtility.UrlEncode(HttpUtility.UrlDecode(term));
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                HttpResponseMessage response = await client.GetAsync(UrlStation + $"?term={normalizedTerm}");
                string rawStations = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatStations(rawStations);
            }
        }

        public async Task<TrainsResponse> GetTrainsAsync(UzContext context, int stationFromId, int stationToId, DateTime date) {
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
               
            TrainsResponse rawTrains = await ExecutePostRequestAsync(context, UrlTrains, content, ResponseFormatter.FormatTrains);
            return rawTrains;
        }

        public async Task<WagonsResponse> GetWagonsAsync(UzContext context, int stationFromId, int stationToId, DateTime date, string trainNumber, int trainType, string wagonTypeCode) {
             var content = new FormUrlEncodedContent(
                new Dictionary<string, string> {
                    {"station_id_from", stationFromId.ToString()},
                    {"station_id_till", stationToId.ToString()},
                    {"date_dep", date.ToUnixTimestamp().ToString(CultureInfo.InvariantCulture)},
                    {"train", trainNumber },
                    {"model", trainType.ToString()},
                    {"coach_type", wagonTypeCode},
                    {"round_trip", "0"},
                    {"another_ec", "0"}
                });

            WagonsResponse wagonsResponse = await ExecutePostRequestAsync(context, UrlWagons, content, ResponseFormatter.FormatWagons);
            return wagonsResponse;
        }

        public async Task<PlacesResponse> GetPlacesAsync(UzContext context, int stationFromId, int stationToId, DateTime date, string trainNumber, int wagonNumber, string coachClass, int coachType) {
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

            PlacesResponse placesResponse = await ExecutePostRequestAsync(context, UrlPlaces, content, ResponseFormatter.FormatPlaces);
            return placesResponse;
        }

        public async Task<BookPlacesResponse> BookPlaceAsync(UzContext context, int stationFromId, int stationToId, DateTime date, string trainNumber, int wagonNumber, string coachClass, string wagonTypeCode, int place, string placeType, string firstName, string lastName) {
            var content = new FormUrlEncodedContent(
                    new Dictionary<string, string> {
                        {"from", stationFromId.ToString()},
                        {"to", stationToId.ToString()},
                        {"date", date.ToString("yyyy-MM-dd")},
                        {"round_trip", "0" },
                        {"train", trainNumber.ToString()},
                        {"places[0][bedding]", "1" },
                        {"places[0][charline]", placeType },
                        {"places[0][child]", "" },
                        {"places[0][firstname]", firstName },
                        {"places[0][lastname]", lastName },
                        {"places[0][ord]", "0" },
                        {"places[0][place_num]", place.ToString() },
                        {"places[0][reserve]", "0" },
                        {"places[0][stud]", "" },
                        {"places[0][transportation]", "0" },
                        {"places[0][wagon_class]", coachClass },
                        {"places[0][wagon_num]", wagonNumber.ToString() },
                        {"places[0][wagon_type]", wagonTypeCode }
                    });

            BookPlacesResponse bookPlacesResponse = await ExecutePostRequestAsync(context, UrlBook, content, ResponseFormatter.FormatBookPlaces);
            bookPlacesResponse.Cookies = context.Cookie.OfType<Cookie>().
                Where(cookie => cookie.Name.StartsWith(UzCookiePrefix)).
                ToDictionary(cookie => cookie.Name, cookie => cookie.Value);
            return bookPlacesResponse;
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

        private async Task<T> ExecutePostRequestAsync<T>(UzContext context, string url, FormUrlEncodedContent content, Func<string, T> formatFunc) {
            string raw;
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                HttpResponseMessage response;
                try {
                     response = await client.PostAsync(url, content);
                } catch (HttpRequestException e) {
                    return Activator.CreateInstance<T>();
                }
                raw = await response.Content.ReadAsStringAsync();
            }
            return FormatRequestResult(raw, formatFunc);
        }

        private static T FormatRequestResult<T>(string raw, Func<string, T> formatFunc) {
            if (string.IsNullOrWhiteSpace(raw)) {
                return Activator.CreateInstance<T>();
            }
            try {
                return formatFunc(raw);
            } catch (Exception e) {
                return Activator.CreateInstance<T>();
            }
        }
    }
}