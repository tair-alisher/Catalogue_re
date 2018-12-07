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
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class DepartmentController : Controller
    {
        private const int ItemsPerPage = 10;
        private IDepartmentService DepartmentService;
        private IAdministrationService AdministrationService;

        public DepartmentController(IDepartmentService depService, IAdministrationService admService)
        {
            DepartmentService = depService;
            AdministrationService = admService;
        }

        public ActionResult AjaxPositionList(int? page)
        {
            var departmentDTOList = DepartmentService.GetAllOrderedByNameWithRelations().ToList();
            var departmentVMList = Mapper.Map<IEnumerable<DepartmentVM>>(departmentDTOList);

            return PartialView("AjaxPositionList", departmentVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

        public ActionResult Index()
        {
            var departmentDTOList = DepartmentService.GetAllOrderedByNameWithRelations().ToList();
            var departmentVMList = Mapper.Map<IEnumerable<DepartmentVM>>(departmentDTOList);

            return View(departmentVMList.ToPagedList(1, ItemsPerPage));
        }

        public ActionResult Details(int? id)
        {
            try
            {
                var departmentDTO = DepartmentService.Get(id);
                var departmentVM = Mapper.Map<DepartmentVM>(departmentDTO);

                return View(departmentVM);
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
            ViewBag.AdministrationId = GetAdministrationIdSelectList();
            return View();
        }

        private SelectList GetAdministrationIdSelectList(int? selectedId = null)
        {
            return new SelectList(AdministrationService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var departmentDTO = Mapper.Map<DepartmentDTO>(model);
                DepartmentService.Add(departmentDTO);

                return RedirectToAction("Index");
            }

            ViewBag.AdministrationId = GetAdministrationIdSelectList(model.AdministrationId);
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                var departmentDTO = DepartmentService.Get(id);
                var departmentVM = Mapper.Map<DepartmentVM>(departmentDTO);
                ViewBag.AdministrationId = GetAdministrationIdSelectList(departmentVM.AdministrationId);

                return View(departmentVM);
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
        public ActionResult Edit(DepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var departmentDTO = Mapper.Map<DepartmentDTO>(model);
                DepartmentService.Update(departmentDTO);

                return RedirectToAction("Index");
            }

            ViewBag.AdministrationId = GetAdministrationIdSelectList(model.AdministrationId);
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var departmentDTO = DepartmentService.Get(id);
                var departmentVM = Mapper.Map<DepartmentVM>(departmentDTO);

                return PartialView(departmentVM);
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
        public ActionResult Delete(int? id, EmployeeVM model)
        {
            try
            {
                DepartmentService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (HasRelationsException)
            {
                return RedirectToRoute(new
                {
                    controller = "Message",
                    action = "Error",
                    message = Messages.HasRelations
                });
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