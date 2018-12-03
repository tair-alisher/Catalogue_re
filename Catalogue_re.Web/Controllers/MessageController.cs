using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class MessageController : Controller
    {
        public ActionResult Error(string message = "Ошибка")
        {
            ViewBag.Message = message;
            return View();
        }
    }
}