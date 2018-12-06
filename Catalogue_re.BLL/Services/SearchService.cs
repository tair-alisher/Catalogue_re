using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoMapper;
using CatalogEntities;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.DAL.Interfaces;

namespace Catalogue_re.BLL.Services
{
    public class SearchService : ISearchService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public SearchService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<EmployeeDTO> GetFilteredEmployeeList(FilterParamsDTO parameters)
        {
            IQueryable<Employee> employees = _unitOfWork.Employees.GetAll().OrderBy(e => e.FullName)
                .Include(e => e.Position)
                .Include(e => e.Department)
                .Include(e => e.Department.Administration)
                .Include(e => e.Department.Administration.Division);

            string name = string.IsNullOrEmpty(parameters.Name) ? "" : parameters.Name.Trim();
            if (!(name.Length <= 0))
                employees = GetEmployeeListFilteredByName(employees, name);

            employees = GetEmployeeListFilteredByParams(employees, parameters);

            return Mapper.Map<IEnumerable<EmployeeDTO>>(employees.ToList());
        }

        private IQueryable<Employee> GetEmployeeListFilteredByName(IQueryable<Employee> employees, string name)
        {
            NameParts parts = new NameParts(name);
            while (parts.Count > 0)
            {
                employees = FilterEmployeeListByName(employees, parts.First());
                parts.RemoveFirst();
            }

            return employees;
        }

        private IQueryable<Employee> FilterEmployeeListByName(IQueryable<Employee> employees, string name)
        {
            return employees.Where(e => e.FullName.Contains(name));
        }

        private IQueryable<Employee> GetEmployeeListFilteredByParams(IQueryable<Employee> employees, FilterParamsDTO parameters)
        {
            if (!string.IsNullOrEmpty(parameters.PositionId))
            {
                int intPositionId = int.Parse(parameters.PositionId);
                employees = employees.Where(e => e.PositionId == intPositionId);
            }

            if (!string.IsNullOrEmpty(parameters.DepartmentId))
            {
                int intDepartmentId = int.Parse(parameters.DepartmentId);
                employees = employees.Where(e => e.DepartmentId == intDepartmentId);
            }

            if (!string.IsNullOrEmpty(parameters.AdministrationId))
            {
                int intAdministrationId = int.Parse(parameters.AdministrationId);
                employees = employees.Where(e => e.Department.AdministrationId == intAdministrationId);
            }

            if (!string.IsNullOrEmpty(parameters.DivisionId))
            {
                int intDivisionId = int.Parse(parameters.DivisionId);
                employees = employees.Where(e => e.Department.Administration.DivisionId == intDivisionId);
            }

            return employees;
        }

        public IEnumerable<DepartmentDTO> GetFilteredDepartmentList(string value)
        {
            if (value.Trim().Length <= 0)
                return Enumerable.Empty<DepartmentDTO>();

            string[] words = value.ToLower().Split(' ');
            List<Department> departments = GetFilteredDepartmentList(words).ToList();

            return Mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        private IQueryable<Department> GetFilteredDepartmentList(string[] words)
        {
            return _unitOfWork.Departments.GetAll()
                .Include(d => d.Administration)
                .Where(d => words.All(d.Name.ToLower().Contains));
        }
    }

    class NameParts
    {
        private const int MaxNumberOfWordsInFullName = 3;
        List<string> Parts;
        public int Count
        {
            get { return Parts.Count(); }
        }

        public NameParts(string name)
        {
            Parts = name.Split(' ').ToList();
            if (Parts.Count() > MaxNumberOfWordsInFullName)
                Parts = Parts.Take(MaxNumberOfWordsInFullName).ToList();
        }

        public string First()
        {
            return Parts.First();
        }

        public void RemoveFirst()
        {
            Parts.Remove(Parts.First());
        }
    }
}
