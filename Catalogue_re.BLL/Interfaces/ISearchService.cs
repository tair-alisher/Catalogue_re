using Catalogue_re.BLL.DTO;
using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<EmployeeDTO> GetFilteredEmployeeList(FilterParamsDTO parameters);
    }
}
