using Catalogue_re.BLL.DTO;
using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface IDepartmentService : IService<DepartmentDTO>
    {
        IEnumerable<DepartmentDTO> GetAllOrderedByName();
        IEnumerable<DepartmentDTO> GetAllOrderedByNameWithRelations();
    }
}
