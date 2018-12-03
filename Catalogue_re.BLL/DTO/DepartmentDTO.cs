using System.Collections.Generic;

namespace Catalogue_re.BLL.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Post { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public int? Code { get; set; }
        public int AdministrationId { get; set; }

        public AdministrationDTO Administration { get; set; }
        public ICollection<EmployeeDTO> Employees { get; set; }
    }
}
