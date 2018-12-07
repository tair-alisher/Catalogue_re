using System.Collections.Generic;

namespace Catalogue_re.BLL.DTO
{
    public class DivisionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AdministrationDTO> Administrations { get; set; }
    }
}
