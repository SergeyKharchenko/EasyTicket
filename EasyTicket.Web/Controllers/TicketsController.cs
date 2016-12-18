using System.Web.Mvc;

namespace EasyTicket.Web.Controllers {
    public class TicketsController : Controller {
        public ActionResult Index() {
            return View();
        }

        public JsonResult Values(string term) {
            return Json(new {
                            results = new[] {
                                new {
                                    id = "CA",
                                    text = "California"
                                }
                            }
                        },
                        JsonRequestBehavior.AllowGet);
        }
    }
}