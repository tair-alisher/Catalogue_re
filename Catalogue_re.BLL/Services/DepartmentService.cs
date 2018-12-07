using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CatalogEntities;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Exceptions;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.DAL.Interfaces;

namespace Catalogue_re.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public DepartmentService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DepartmentDTO> GetAll()
        {
            var departments = _unitOfWork.Departments.GetAll().ToList();

            return Mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        public IEnumerable<DepartmentDTO> GetAllOrderedByName()
        {
            var departments = _unitOfWork.Departments.GetAll().OrderBy(d => d.Name).ToList();

            return Mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        public IEnumerable<DepartmentDTO> GetAllOrderedByNameWithRelations()
        {
            var departments = _unitOfWork.Departments.GetAll().OrderBy(d => d.Name).Include(d => d.Administration).ToList();

            return Mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        public DepartmentDTO Get(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var department = _unitOfWork.Departments.GetAll().Where(d => d.Id == id).Include(d => d.Administration).FirstOrDefault();
            if (department == null)
                throw new NotFoundException();

            return Mapper.Map<DepartmentDTO>(department);
        }

        public void Add(DepartmentDTO item)
        {
            var department = Mapper.Map<Department>(item);
            _unitOfWork.Departments.Create(department);
            _unitOfWork.Save();
        }

        public void Update(DepartmentDTO item)
        {
            var department = Mapper.Map<Department>(item);
            _unitOfWork.Departments.Update(department);
            _unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            if (HasRelations((int)id))
                throw new HasRelationsException();

            var department = _unitOfWork.Departments.Get(id);
            if (department == null)
                throw new NotFoundException();

            _unitOfWork.Departments.Delete((int)id);
            _unitOfWork.Save();
        }

        private bool HasRelations(int id)
        {
            var relationsCount = _unitOfWork.Employees.Find(e => e.DepartmentId == id).Count();

            return relationsCount > 0;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
