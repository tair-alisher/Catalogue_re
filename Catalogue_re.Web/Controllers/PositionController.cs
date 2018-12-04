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
    public class PositionController : Controller
    {
        private const int ItemsPerPage = 10;
        private IPositionService PositionService;

        public PositionController(IPositionService posService)
        {
            PositionService = posService;
        }

        public ActionResult AjaxPositionList(int? page)
        {
            var positionDTOList = PositionService.GetAllOrderedByName().ToList();
            var positionVMList = Mapper.Map<IEnumerable<PositionVM>>(positionDTOList);

            return PartialView(positionVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

        public ActionResult Index()
        {
            var positionDTOList = PositionService.GetAllOrderedByName().ToList();
            var positionVMList = Mapper.Map<IEnumerable<PositionVM>>(positionDTOList);

            return View(positionVMList.ToPagedList(1, ItemsPerPage));
        }

        public ActionResult Details(int? id)
        {
            try
            {
                var positionDTO = PositionService.Get(id);
                var positionVM = Mapper.Map<PositionVM>(positionDTO);

                return View(positionVM);
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
        public ActionResult Create(PositionVM model)
        {
            if (ModelState.IsValid)
            {
                var positionDTO = Mapper.Map<PositionDTO>(model);
                PositionService.Add(positionDTO);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                var positionDTO = PositionService.Get(id);
                var positionVM = Mapper.Map<PositionVM>(positionDTO);

                return View(positionVM);
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
        public ActionResult Edit(PositionVM model)
        {
            if (ModelState.IsValid)
            {
                var positionDTO = Mapper.Map<PositionDTO>(model);
                PositionService.Update(positionDTO);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var positionDTO = PositionService.Get(id);
                var positionVM = Mapper.Map<PositionVM>(positionDTO);

                return PartialView(positionVM);
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
        public ActionResult Delete(int? id, PositionVM model)
        {
            try
            {
                PositionService.Delete(id);
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