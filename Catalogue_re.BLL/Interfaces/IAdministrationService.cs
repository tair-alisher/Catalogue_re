using Catalogue_re.BLL.DTO;
using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface IAdministrationService : IService<AdministrationDTO>
    {
        IEnumerable<AdministrationDTO> GetAllOrderedByName();
        IEnumerable<AdministrationDTO> GetAllOrderedByNameWithRelations();
    }
}
