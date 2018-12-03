using Catalogue_re.BLL.Interfaces;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class EmployeeController : BaseController
    {
        private const int ItemsPerPage = 10;

        public EmployeeController(IEmployeeService empService, IDepartmentService depService,
            IPositionService posService, IAdministrationService admService, IDivisionService divService) : base(empService, posService, depService, admService, divService) { }
    }
}