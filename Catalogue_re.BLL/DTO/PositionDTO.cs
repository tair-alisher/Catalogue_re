using System.Collections.Generic;

namespace Catalogue_re.BLL.DTO
{
    public class PositionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeDTO> Employees { get; set; }
    }
}
