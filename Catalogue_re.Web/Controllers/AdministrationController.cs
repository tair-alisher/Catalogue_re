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
using System.Web.UI;

namespace Catalogue_re.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {
        private const int ItemsPerPage = 10;
        private IAdministrationService AdministrationService;
        private IDivisionService DivisionService;

        public AdministrationController(IAdministrationService admService, IDivisionService divService)
        {
            AdministrationService = admService;
            DivisionService = divService;
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult AjaxPositionList(int? page)
        {
            var administrationDTOList = AdministrationService.GetAllOrderedByNameWithRelations().ToList();
            var administrationVMList = Mapper.Map<IEnumerable<AdministrationVM>>(administrationDTOList);

            return PartialView(administrationVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult Index()
        {
            var administrationDTOList = AdministrationService.GetAllOrderedByNameWithRelations().ToList();
            var administrationVMList = Mapper.Map<IEnumerable<AdministrationVM>>(administrationDTOList);

            return View(administrationVMList.ToPagedList(1, ItemsPerPage));
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult Details(int? id)
        {
            try
            {
                var administrationDTO = AdministrationService.Get(id);
                var administrationVM = Mapper.Map<AdministrationVM>(administrationDTO);

                return View(administrationVM);
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
            ViewBag.DivisionId = GetDivisionIdSelectList();
            return View();
        }

        
        private SelectList GetDivisionIdSelectList(int? selectedId = null)
        {
            return new SelectList(DivisionService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdministrationVM model)
        {
            if (ModelState.IsValid)
            {
                var administrationDTO = Mapper.Map<AdministrationDTO>(model);
                AdministrationService.Add(administrationDTO);

                return RedirectToAction("Index");
            }

            ViewBag.DivisionId = GetDivisionIdSelectList(model.DivisionId);
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                var administrationDTO = AdministrationService.Get(id);
                var administrationVM = Mapper.Map<AdministrationVM>(administrationDTO);
                ViewBag.DivisionId = GetDivisionIdSelectList(administrationVM.DivisionId);

                return View(administrationVM);
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
        public ActionResult Edit(AdministrationVM model)
        {
            if (ModelState.IsValid)
            {
                var administrationDTO = Mapper.Map<AdministrationDTO>(model);
                AdministrationService.Update(administrationDTO);

                return RedirectToAction("Index");
            }

            ViewBag.DivisionId = GetDivisionIdSelectList(model.DivisionId);
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var administrationDTO = AdministrationService.Get(id);
                var administrationVM = Mapper.Map<AdministrationVM>(administrationDTO);

                return PartialView(administrationVM);
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
        public ActionResult Delete(int? id, AdministrationVM model)
        {
            try
            {
                AdministrationService.Delete(id);
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
                    cotnroller = "Message",
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