using AutoMapper;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class HomeController : BaseController
    {
        private const int ItemsPerPage = 10;

        public HomeController(IEmployeeService empService, IDepartmentService depService,
            IPositionService posService, IAdministrationService admService, IDivisionService divService) : base(empService, posService, depService, admService, divService) { }

        public ActionResult Index(int? page)
        {
            ViewBag.PositionId = GetPositionIdSelecteList();
            ViewBag.DepartmentId = GetDepartmentIdSelectList();
            ViewBag.AdministrationId = GetAdministrationIdSelectList();
            ViewBag.DivisionId = GetDivisionIdSelectList();

            var employeeDTOList = EmployeeService.GetAllOrderedByNameWithRelations().ToList();
            var employeeVMList = Mapper.Map<IEnumerable<EmployeeVM>>(employeeDTOList);

            string view = "Index";
            if (User.IsInRole("manager") || User.IsInRole("administrator"))
                view = "ManagerIndex";

            return View(view, employeeVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }
    }
}