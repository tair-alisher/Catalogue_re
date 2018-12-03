using Catalogue_re.BLL.DTO;
using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface IDivisionService : IService<DivisionDTO>
    {
        IEnumerable<DivisionDTO> GetAllOrderedByName();
    }
}
