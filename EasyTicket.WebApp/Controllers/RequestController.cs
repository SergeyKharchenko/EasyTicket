using EasyTicket.WebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EasyTicket.WebApp.Controllers {
    public class RequestController : Controller {
        private readonly Config _config;

        public RequestController(Config config) {
            _config = config;
        }

        public IActionResult Index() {
            return View(_config);
        }
    }
}