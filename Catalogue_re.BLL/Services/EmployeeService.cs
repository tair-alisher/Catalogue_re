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
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public EmployeeService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employees = _unitOfWork.Employees.GetAll().ToList();

            return Mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public IEnumerable<EmployeeDTO> GetAllOrderedByName()
        {
            var employees = _unitOfWork.Employees.GetAll().OrderBy(e => e.FullName).ToList();

            return Mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public IEnumerable<EmployeeDTO> GetAllOrderedByNameWithRelations()
        {
            var employees = _unitOfWork.Employees.GetAll().OrderBy(e => e.FullName)
                .Include(e => e.Department).Include(e => e.Position)
                .Include(e => e.Department.Administration)
                .Include(e => e.Department.Administration.Division).ToList();

                return Mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public EmployeeDTO Get(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var employee = _unitOfWork.Employees.GetAll().Where(e => e.Id == id)
                .Include(e => e.Department).Include(e => e.Department.Administration).Include(e => e.Position).FirstOrDefault();
            if (employee == null)
                throw new NotFoundException();

            return Mapper.Map<EmployeeDTO>(employee);
        }

        public void Add(EmployeeDTO item)
        {
            var employee = Mapper.Map<Employee>(item);
            _unitOfWork.Employees.Create(employee);
            _unitOfWork.Save();
        }

        public void Update(EmployeeDTO item)
        {
            var employee = Mapper.Map<Employee>(item);
            _unitOfWork.Employees.Update(employee);
            _unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();

            var employee = _unitOfWork.Employees.Get(id);
            if (employee == null)
                throw new NotFoundException();

            _unitOfWork.Employees.Delete((int)id);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
