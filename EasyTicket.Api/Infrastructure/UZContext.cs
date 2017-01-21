using System.Net;

namespace EasyTicket.Api.Infrastructure {
    public class UZContext
    {
        public string Token { get; private set; }

        public CookieCollection Cookie { get; private set; }

        public UZContext(string token, CookieCollection cookie)
        {
            Token = token;
            Cookie = cookie;
        }
    }
}