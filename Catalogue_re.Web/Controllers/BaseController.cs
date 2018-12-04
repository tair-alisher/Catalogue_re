using AutoMapper;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    public class BaseController : Controller
    {
        public IEmployeeService EmployeeService;
        public IPositionService PositionService;
        public IDepartmentService DepartmentService;
        public IAdministrationService AdministrationService;
        public IDivisionService DivisionService;

        public BaseController(IEmployeeService empService, IPositionService posService, IDepartmentService depService,
            IAdministrationService admService, IDivisionService divService)
        {
            EmployeeService = empService;
            PositionService = posService;
            DepartmentService = depService;
            AdministrationService = admService;
            DivisionService = divService;
        }

        public SelectList GetPositionIdSelecteList(int? selectedId = null)
        {
            return new SelectList(PositionService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        public SelectList GetDepartmentIdSelectList(int? selectedId = null)
        {
            return new SelectList(DepartmentService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        public SelectList GetComplexDepartmentIdSelectList(int? selectedId = null)
        {
            var departmentDTOList = DepartmentService.GetAllOrderedByNameWithRelations().ToList();
            var departmentVMList = Mapper.Map<IEnumerable<DepartmentVM>>(departmentDTOList).ToList();

            return new SelectList(departmentVMList, "Id", "DepartmentWithAdministration", selectedId);
        }

        public SelectList GetAdministrationIdSelectList(int? selectedId = null)
        {
            return new SelectList(AdministrationService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        public SelectList GetDivisionIdSelectList(int? selectedId = null)
        {
            return new SelectList(DivisionService.GetAllOrderedByName().ToList(), "Id", "Name", selectedId);
        }

        public string UploadImage(HttpPostedFileBase photo)
        {
            var filename = Path.GetFileName(photo.FileName);
            filename = GenerateUniqueName(filename);
            string directory = Server.MapPath(Url.Content("~/images"));
            string savePath = Path.Combine(directory, filename);
            photo.SaveAs(savePath);

            return filename;
        }

        private string GenerateUniqueName(string filePath)
        {
            string date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string extension = filePath.Split('.').Last().ToLower();

            return $"{date}.{extension}";
        }

        public string UpdateImage(string oldImage, HttpPostedFileBase image)
        {
            RemoveImage(oldImage);
            return UploadImage(image);
        }

        public void RemoveImage(string image)
        {
            string imagePath = Request.MapPath("~/images/" + image);
            if (System.IO.File.Exists(imagePath) && image != "default-avatar.png")
                System.IO.File.Delete(imagePath);
        }
    }
}