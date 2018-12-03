using Catalogue_re.BLL.DTO;
using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface IPositionService : IService<PositionDTO>
    {
        IEnumerable<PositionDTO> GetAllOrderedByName();
    }
}
