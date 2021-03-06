﻿using AutoMapper;
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
    public class DivisionController : Controller
    {
        private const int ItemsPerPage = 10;
        private IDivisionService DivisionService;

        public DivisionController(IDivisionService divisionService)
        {
            DivisionService = divisionService;
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult AjaxPositionList(int? page)
        {
            var divisionDTOList = DivisionService.GetAllOrderedByName().ToList();
            var divisionVMList = Mapper.Map<IEnumerable<DivisionVM>>(divisionDTOList);

            return PartialView(divisionVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult Index()
        {
            var divisionDTOList = DivisionService.GetAllOrderedByName().ToList();
            var divisionVMList = Mapper.Map<IEnumerable<DivisionVM>>(divisionDTOList);

            return View(divisionVMList.ToPagedList(1, ItemsPerPage));
        }

        [OutputCache(Duration = 30, Location = OutputCacheLocation.Downstream)]
        public ActionResult Details(int? id)
        {
            try
            {
                var divisionDTO = DivisionService.Get(id);
                var divisionVM = Mapper.Map<DivisionVM>(divisionDTO);

                return View(divisionVM);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DivisionVM model)
        {
            if (ModelState.IsValid)
            {
                var divisionDTO = Mapper.Map<DivisionDTO>(model);
                DivisionService.Add(divisionDTO);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                var divisionDTO = DivisionService.Get(id);
                var divisionVM = Mapper.Map<DivisionVM>(divisionDTO);

                return View(divisionVM);
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
        public ActionResult Edit(DivisionVM model)
        {
            if (ModelState.IsValid)
            {
                var divisionDTO = Mapper.Map<DivisionDTO>(model);
                DivisionService.Update(divisionDTO);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var divisionDTO = DivisionService.Get(id);
                var divisionVM = Mapper.Map<DivisionVM>(divisionDTO);

                return PartialView(divisionVM);
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
        public ActionResult Delete(int? id, DivisionVM model)
        {
            try
            {
                DivisionService.Delete(id);
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