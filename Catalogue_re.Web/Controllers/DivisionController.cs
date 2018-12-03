using AutoMapper;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Exceptions;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class DivisionController : Controller
    {
        private const int ItemsPerPage = 10;
        private IDivisionService DivisionService;

        public DivisionController(IDivisionService divisionService)
        {
            DivisionService = divisionService;
        }

        public ActionResult Index(int? page)
        {
            var divisionDTOList = DivisionService.GetAllOrderedByName().ToList();
            var divisionVMList = Mapper.Map<IEnumerable<DivisionVM>>(divisionDTOList);

            return View(divisionVMList.ToPagedList(page ?? 1, ItemsPerPage));
        }

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
                return HttpNotFound();
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
                return HttpNotFound();
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
                    message = "Имеются связи. Удаление невозможно."
                });
            }
        }
    }
}