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
            string name = Request.QueryString["name"];
            string page = Request.QueryString["page"];
            string positionId = Request.QueryString["PositionId"];
            string departmentId = Request.QueryString["DepartmentId"];
            string administrationId = Request.QueryString["AdministrationId"];
            string divisionId = Request.QueryString["DivisionId"];

            FilterParamsDTO parameters = new FilterParamsDTO();

            var employeeDTOList = SearchService.GetFilteredEmployeeList(parameters).ToList();
            var employeeVMList = Mapper.Map<IEnumerable<EmployeeVM>>(employeeDTOList);
            string view = "EmployeeFilter";
            if (User.IsInRole("admin"))
                view = "AdminEmployeeFilter";
            else if (User.IsInRole("manager"))
                view = "ManagerEmployeeFilter";

            return PartialView(view, employeeVMList.ToPagedList(page == null ? 1 : int.Parse(page), ItemsPerPage));
        }


    }
}