using AutoMapper;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Exceptions;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.ViewModels;
using Catalogue_re.Web.Util;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class EmployeeController : BaseController
    {
        private const int ItemsPerPage = 10;

        public EmployeeController(IEmployeeService empService, IDepartmentService depService,
            IPositionService posService, IAdministrationService admService, IDivisionService divService) : base(empService, posService, depService, admService, divService) { }

        public ActionResult Index(int? page)
        {
            ViewBag.PositionId = GetPositionIdSelecteList();
            ViewBag.DepartmentId = GetDepartmentIdSelectList();
            ViewBag.AdministrationId = GetAdministrationIdSelectList();
            ViewBag.DivisionId = GetDivisionIdSelectList();

            var employeeDTOList = EmployeeService.GetAllOrderedByNameWithRelations().ToList();
            var employeeVMList = Mapper.Map<IEnumerable<EmployeeVM>>(employeeDTOList);

            return View(employeeVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

        public ActionResult Details(int? id)
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
                    action = "Error",
                    message = Messages.NotFound
                });
            }
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = GetComplexDepartmentIdSelectList();
            ViewBag.PositionId = GetPositionIdSelecteList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeVM model, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                var employeeDTO = Mapper.Map<EmployeeDTO>(model);
                employeeDTO.Photo = "default-avatar.png";
                if (photo != null)
                    employeeDTO.Photo = UploadImage(photo);
                EmployeeService.Add(employeeDTO);

                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = GetComplexDepartmentIdSelectList(model.DepartmentId);
            ViewBag.PositionId = GetPositionIdSelecteList(model.PositionId);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                var employeeDTO = EmployeeService.Get(id);
                var employeeVM = Mapper.Map<EmployeeVM>(employeeDTO);
                ViewBag.DepartmentId = GetComplexDepartmentIdSelectList(employeeVM.DepartmentId);
                ViewBag.PositionId = GetPositionIdSelecteList(employeeVM.PositionId);

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
                    action = "Error",
                    message = Messages.NotFound
                });
            }
        }
    }
}