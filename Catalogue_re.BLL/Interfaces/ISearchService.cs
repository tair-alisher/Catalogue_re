using Catalogue_re.BLL.DTO;
using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<EmployeeDTO> GetFilteredEmployeeList(FilterParamsDTO parameters);
        IEnumerable<DivisionDTO> GetFilteredDivisionList(string value);
        IEnumerable<AdministrationDTO> GetFilteredAdministrationList(string value);
        IEnumerable<DepartmentDTO> GetFilteredDepartmentList(string value);
        IEnumerable<PositionDTO> GetFilteredPositionList(string value);
    }
}
