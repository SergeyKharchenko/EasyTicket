using System.Net;

namespace EasyTicket.SharedResources.Infrastructure {
    public class UzContext {
        public string Token { get; private set; }

        public CookieCollection Cookie { get; private set; }

        public UzContext(string token, CookieCollection cookie) {
            Token = token;
            Cookie = cookie;
        }
    }
}