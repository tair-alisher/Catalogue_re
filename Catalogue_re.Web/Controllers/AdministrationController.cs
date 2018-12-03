using Catalogue_re.BLL.Interfaces;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private const int ItemsPerPage = 10;
        private IAdministrationService AdministrationService;

        public AdministrationController(IAdministrationService admService)
        {
            AdministrationService = admService;
        }

        public ActionResult Index(int? page)
        {
            var administrationDTOList = AdministrationService.Get
            return View();
        }
    }
}