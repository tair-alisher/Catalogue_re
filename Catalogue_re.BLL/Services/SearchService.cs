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

        public IEnumerable<DivisionDTO> GetFilteredDivisionList(string value)
        {
            if (value.Trim().Length <= 0)
                return Enumerable.Empty<DivisionDTO>();

            List<string> words = value.Split(' ').ToList();
            List<Division> divisions = GetFilteredDivisionList(words).ToList();

            return Mapper.Map<IEnumerable<DivisionDTO>>(divisions);
        }

        private IQueryable<Division> GetFilteredDivisionList(List<string> words, IQueryable<Division> divisions = null)
        {
            if (words.Count() > 0)
            {
                string word = words.First();
                divisions = (divisions ?? _unitOfWork.Divisions.GetAll()).Where(d => d.Name.Contains(word));
                words.Remove(words.First());
                return GetFilteredDivisionList(words, divisions);
            }

            return divisions;
        }

        public IEnumerable<AdministrationDTO> GetFilteredAdministrationList(string value)
        {
            if (value.Trim().Length <= 0)
                return Enumerable.Empty<AdministrationDTO>();

            List<string> words = value.Split(' ').ToList();
            List<Administration> administrations = GetFilteredAdministrationList(words).ToList();

            return Mapper.Map<IEnumerable<AdministrationDTO>>(administrations);
        }

        private IQueryable<Administration> GetFilteredAdministrationList(List<string> words, IQueryable<Administration> administrations = null)
        {
            if (words.Count() > 0)
            {
                string word = words.First();
                administrations = (administrations ?? _unitOfWork.Administrations.GetAll().Include(a => a.Division)).Where(a => a.Name.Contains(word));
                words.Remove(words.First());
                return GetFilteredAdministrationList(words, administrations);
            }

            return administrations;
        }

        public IEnumerable<DepartmentDTO> GetFilteredDepartmentList(string value)
        {
            if (value.Trim().Length <= 0)
                return Enumerable.Empty<DepartmentDTO>();

            List<string> words = value.Split(' ').ToList();
            List<Department> departments = GetFilteredDepartmentList(words).ToList();

            return Mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        private IQueryable<Department> GetFilteredDepartmentList(List<string> words, IQueryable<Department> departments = null)
        {
            if (words.Count() > 0)
            {
                string word = words.First();
                departments = (departments ?? _unitOfWork.Departments.GetAll().Include(d => d.Administration)).Where(d => d.Name.Contains(word));
                words.Remove(words.First());
                return GetFilteredDepartmentList(words, departments);
            }

            return departments;
        }

        public IEnumerable<PositionDTO> GetFilteredPositionList(string value)
        {
            if (value.Trim().Length <= 0)
                return Enumerable.Empty<PositionDTO>();

            List<string> words = value.Split(' ').ToList();
            List<Position> positions = GetFilteredPositionList(words).ToList();

            return Mapper.Map<IEnumerable<PositionDTO>>(positions);
        }

        private IQueryable<Position> GetFilteredPositionList(List<string> words, IQueryable<Position> positions = null)
        {
            if (words.Count() > 0)
            {
                string word = words.First();
                positions = (positions ?? _unitOfWork.Positions.GetAll()).Where(p => p.Name.Contains(word));
                words.Remove(words.First());
                return GetFilteredPositionList(words, positions);
            }

            return positions;
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
