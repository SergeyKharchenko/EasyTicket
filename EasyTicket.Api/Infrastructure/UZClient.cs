﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EasyTicket.Api.Infrastructure {
    public class UZClient {
        private const string BaseUrl = @"http://booking.uz.gov.ua/ru/";
        private const string UrlStation = BaseUrl + @"purchase/station/";
        private const string UrlTrains = BaseUrl + @"purchase/search/";

        private readonly TokenDecoder TokenDecoder = new TokenDecoder();
        private readonly ResponseFormatter ResponseFormatter = new ResponseFormatter();

        public async Task<UZContext> GetUZContext() {
            using (CookieSupportatbleHttpClient client = CreateHttpClient()) {
                HttpResponseMessage response = await client.GetAsync(BaseUrl);
                string mainPageHtml = await response.Content.ReadAsStringAsync();
                string token = TokenDecoder.Decode(mainPageHtml);
                CookieCollection cookies = client.ReadCookies(response);
                return new UZContext(token, cookies);
            }
        }

        public async Task<string> GetStations(UZContext context, string term) {
            string normalizedTerm = HttpUtility.UrlEncode(HttpUtility.UrlDecode(term));
            using (CookieSupportatbleHttpClient client = CreateHttpClient(context)) {
                HttpResponseMessage response = await client.GetAsync(UrlStation + normalizedTerm + "/");
                string rawStations = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatStations(rawStations);
            }
        }

        public async Task<string> GetTrains(UZContext context, int stationIdFrom, int stationIdTo, DateTime date) {
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
                return await response.Content.ReadAsStringAsync();
            }
        }

        private CookieSupportatbleHttpClient CreateHttpClient(UZContext context = null) {
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