using System;

namespace Catalogue_re.BLL.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Room { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PersonalPhone { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Photo { get; set; }
        public DateTime? DateAdoption { get; set; }
        public DateTime? DateDismissal { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }

        public PositionDTO Position { get; set; }
        public DepartmentDTO Department { get; set; }
    }
}
