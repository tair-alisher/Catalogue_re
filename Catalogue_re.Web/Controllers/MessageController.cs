using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class MessageController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult Error(string message = "Ошибка")
        {
            ViewBag.Message = message;
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult PartialError(string message = "Ошибка")
        {
            ViewBag.Message = message;
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult AnonymousError(string message = "Ошибка")
        {
            ViewBag.Message = message;
            return View();
        }
    }
}