using Catalogue_re.BLL.DTO;
using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface IEmployeeService : IService<EmployeeDTO>
    {
        IEnumerable<EmployeeDTO> GetAllOrderedByName();
        IEnumerable<EmployeeDTO> GetAllOrderedByNameWithRelations();
    }
}
