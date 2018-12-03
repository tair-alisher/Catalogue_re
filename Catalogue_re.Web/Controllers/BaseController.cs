using Catalogue_re.BLL.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class BaseController : Controller
    {
        public IEmployeeService EmployeeService;
        public IPositionService PositionService;
        public IDepartmentService DepartmentService;
        public IAdministrationService AdministrationService;
        public IDivisionService DivisionService;

        public BaseController(IEmployeeService empService, IPositionService posService, IDepartmentService depService,
            IAdministrationService admService, IDivisionService divService)
        {
            EmployeeService = empService;
            PositionService = posService;
            DepartmentService = depService;
            AdministrationService = admService;
            DivisionService = divService;
        }

        public SelectList GetPositionIdSelecteList(int? selectedId = null)
        {
            return new SelectList(PositionService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        public SelectList GetDepartmentIdSelectList(int? selectedId = null)
        {
            return new SelectList(DepartmentService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        public SelectList GetAdministrationIdSelectList(int? selectedId = null)
        {
            return new SelectList(AdministrationService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        public SelectList GetDivisionIdSelectList(int? selectedId = null)
        {
            return new SelectList(DivisionService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }
    }
}