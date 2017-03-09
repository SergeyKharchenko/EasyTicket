using Microsoft.AspNetCore.Mvc;

namespace EasyTicket.WebApp.Controllers {
    public class RequestController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}