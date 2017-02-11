using System.Web.Mvc;

namespace EasyTicket.Web.Controllers {
    public class TicketsController : Controller {
        public ActionResult Index() {
            return View();
        }

        public JsonResult Values(string term) {

            Content("", "");

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


        public ActionResult Buy() {
            return View();
        }
    }
}