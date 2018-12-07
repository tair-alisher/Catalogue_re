using AutoMapper;
using Catalogue_re.BLL.Exceptions;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.ViewModels;
using Catalogue_re.Web.Util;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class HomeController : BaseController
    {
        // TODO: CreateEmployee - Image uploading
        // TODO: OutputCache
        // TODO: change defaultRole to manager
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
            if (User.IsInRole("manager") || User.IsInRole("admin"))
                view = "ManagerIndex";

            return View(view, employeeVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

        [Authorize(Roles = "manager, admin")]
        public ActionResult EmployeeDetails(int? id)
        {
            try
            {
                var employeeDTO = EmployeeService.Get(id);
                var employeeVM = Mapper.Map<EmployeeVM>(employeeDTO);

                return View(employeeVM);
            }
            catch (ArgumentNullException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (NotFoundException)
            {
                return RedirectToRoute(new
                {
                    controller = "Message",
                    action = "AnonymousError",
                    message = Messages.NotFound
                });
            }
        }
    }
}