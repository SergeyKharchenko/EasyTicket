using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace EasyTicket.SharedResources {
    public class CookieSupportatbleHttpClient : HttpClient {
        private readonly CookieContainer _cookie;

        public CookieSupportatbleHttpClient() : this(new CookieContainer()) {}

        private CookieSupportatbleHttpClient(CookieContainer cookie) : base(new HttpClientHandler {
                                                                                CookieContainer = cookie
                                                                            }) {
            _cookie = cookie;
        }

        public void SetCookie(CookieCollection cookie) {
            _cookie.Add(cookie);
        }

        public CookieCollection ReadCookies(HttpResponseMessage response) {
            Uri pageUri = response.RequestMessage.RequestUri;

            var cookieContainer = new CookieContainer();
            IEnumerable<string> cookies;
            if (response.Headers.TryGetValues("set-cookie", out cookies)) {
                foreach (string cookieHeader in cookies) {
                    cookieContainer.SetCookies(pageUri, cookieHeader);
                }
            }

            return cookieContainer.GetCookies(pageUri);
        }
    }
}