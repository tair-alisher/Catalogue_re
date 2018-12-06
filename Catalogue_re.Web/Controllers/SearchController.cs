using AutoMapper;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class SearchController : Controller
    {
        private const int ItemsPerPage = 10;
        private ISearchService SearchService;

        public SearchController(ISearchService searchService)
        {
            SearchService = searchService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeFilter()
        {
            string name = Request.Form["name"];
            string page = Request.Form["page"];
            string positionId = Request.Form["PositionId"];
            string departmentId = Request.Form["DepartmentId"];
            string administrationId = Request.Form["AdministrationId"];
            string divisionId = Request.Form["DivisionId"];

            FilterParamsDTO parameters = new FilterParamsDTO
            {
                Name = name,
                PositionId = positionId,
                DepartmentId = departmentId,
                AdministrationId = administrationId,
                DivisionId = divisionId
            };

            var employeeDTOList = SearchService.GetFilteredEmployeeList(parameters).ToList();
            var employeeVMList = Mapper.Map<IEnumerable<EmployeeVM>>(employeeDTOList);
            string view = "EmployeeFilter";
            if (User.IsInRole("admin"))
                view = "AdminEmployeeFilter";
            else if (User.IsInRole("manager"))
                view = "ManagerEmployeeFilter";

            return PartialView(view, employeeVMList.ToPagedList(page == null ? 1 : int.Parse(page), ItemsPerPage));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DivisionFilter(string value)
        {
            var divisionDTOList = SearchService.GetFilteredDivisionList(value).ToList();
            var divisionVMList = Mapper.Map<IEnumerable<DivisionVM>>(divisionDTOList);

            return PartialView(divisionVMList.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdministrationFilter(string value)
        {
            var administrationDTOList = SearchService.GetFilteredAdministrationList(value).ToList();
            var administrationVMList = Mapper.Map<IEnumerable<AdministrationVM>>(administrationDTOList);

            return PartialView(administrationVMList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentFilter(string value)
        {
            var departmentDTOList = SearchService.GetFilteredDepartmentList(value).ToList();
            var departmentVMList = Mapper.Map<IEnumerable<DepartmentVM>>(departmentDTOList);

            return PartialView(departmentVMList.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PositionFilter(string value)
        {
            var positionDTOList = SearchService.GetFilteredPositionList(value).ToList();
            var positionVMList = Mapper.Map<IEnumerable<PositionVM>>(positionDTOList);

            return PartialView(positionVMList.ToList());
        }
    }
}