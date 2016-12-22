using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EasyTicket.Api.Infrastructure {
    public class UZClient {
        private const string BaseUrl = @"http://booking.uz.gov.ua/";
        private const string UrlMain = @"ru/";
        private const string UrlStation = @"ru/purchase/station/";

        private readonly TokenDecoder TokenDecoder = new TokenDecoder();
        private readonly ResponseFormatter ResponseFormatter = new ResponseFormatter();

        public async Task<string> GetToken() {
            using (HttpClient client = CreateHttpClient()) {
                HttpResponseMessage response = await client.GetAsync(UrlMain);
                string mainPageHtml = await response.Content.ReadAsStringAsync();
                return TokenDecoder.Decode(mainPageHtml);
            }
        }

        public async Task<string> GetStations(string token, string term) {
            string normalizedTerm = HttpUtility.UrlEncode(HttpUtility.UrlDecode(term));
            using (HttpClient client = CreateHttpClient(token)) {
                HttpResponseMessage response = await client.GetAsync(UrlStation + normalizedTerm + "/");
                string rawStations = await response.Content.ReadAsStringAsync();
                return ResponseFormatter.FormatStations(rawStations);
            }
        }

        private HttpClient CreateHttpClient(string token = "") {
            var httpClient = new HttpClient {
                BaseAddress = new Uri(BaseUrl)
            };

            if (!string.IsNullOrEmpty(token)) {
                httpClient.DefaultRequestHeaders.Add("GV-Ajax", "1");
                httpClient.DefaultRequestHeaders.Add("GV-Referer", BaseUrl);
                httpClient.DefaultRequestHeaders.Add("GV-Token", token);
            }

            return httpClient;
        }

        
    }
}