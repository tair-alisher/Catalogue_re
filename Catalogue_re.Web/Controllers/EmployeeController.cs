using AutoMapper;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Exceptions;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.ViewModels;
using Catalogue_re.Web.Util;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Catalogue_re.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class EmployeeController : BaseController
    {
        private const int ItemsPerPage = 10;

        public EmployeeController(IEmployeeService empService, IDepartmentService depService,
            IPositionService posService, IAdministrationService admService, IDivisionService divService) : base(empService, posService, depService, admService, divService) { }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult AjaxPositionList(int? page)
        {
            var employeeDTOList = EmployeeService.GetAllOrderedByNameWithRelations().ToList();
            var employeeVMList = Mapper.Map<IEnumerable<EmployeeVM>>(employeeDTOList);

            return PartialView(employeeVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult Index()
        {
            ViewBag.PositionId = GetPositionIdSelecteList();
            ViewBag.DepartmentId = GetDepartmentIdSelectList();
            ViewBag.AdministrationId = GetAdministrationIdSelectList();
            ViewBag.DivisionId = GetDivisionIdSelectList();

            var employeeDTOList = EmployeeService.GetAllOrderedByNameWithRelations().ToList();
            var employeeVMList = Mapper.Map<IEnumerable<EmployeeVM>>(employeeDTOList);

            return View(employeeVMList.ToPagedList(1, ItemsPerPage));
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
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
        public ActionResult Create(EmployeeVM model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                model.Photo = "default-avatar.png";
                if (image != null)
                    model.Photo = UploadImage(image);
                var employeeDTO = Mapper.Map<EmployeeDTO>(model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeVM model, HttpPostedFileBase image, string oldImage)
        {
            if (ModelState.IsValid)
            {
                model.Photo = oldImage;
                if (image != null)
                    model.Photo = UpdateImage(oldImage, image);
                var employeeDTO = Mapper.Map<EmployeeDTO>(model);
                EmployeeService.Update(employeeDTO);

                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = GetComplexDepartmentIdSelectList(model.DepartmentId);
            ViewBag.PositionId = GetPositionIdSelecteList(model.PositionId);

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var employeeDTO = EmployeeService.Get(id);
                var employeeVM = Mapper.Map<EmployeeVM>(employeeDTO);

                return PartialView(employeeVM);
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
                    action = "PartialError",
                    message = Messages.NotFound
                });
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, string image)
        {
            try
            {
                RemoveImage(image);
                EmployeeService.Delete(id);

                return RedirectToAction("Index");
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