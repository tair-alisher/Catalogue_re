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
        public ActionResult EmployeeFilter()
        {
            string name = Request.QueryString["name"];
            string page = Request.QueryString["page"];
            string positionId = Request.QueryString["positionId"];
            string departmentId = Request.QueryString["departmentId"];
            string administrationId = Request.QueryString["administrationId"];
            string divisionId = Request.QueryString["divisionId"];

            FilterParamsDTO parameters = new FilterParamsDTO();

            var employeeDTOList = SearchService.GetFilteredEmployeeList(parameters).ToList();
            var employeeVMList = Mapper.Map<IEnumerable<EmployeeVM>>(employeeDTOList);
            string view = "EmployeeFilter";

            return View(view, employeeVMList.ToPagedList(page == null ? 1 : int.Parse(page), ItemsPerPage));
        }
    }
}