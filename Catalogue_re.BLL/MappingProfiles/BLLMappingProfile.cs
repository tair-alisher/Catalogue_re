using AutoMapper;
using CatalogEntities;
using Catalogue_re.BLL.DTO;

namespace Catalogue_re.BLL.MappingProfiles
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateMap<Division, DivisionDTO>(MemberList.None).ReverseMap();
            CreateMap<Administration, AdministrationDTO>(MemberList.None).ReverseMap();
            CreateMap<Department, DepartmentDTO>(MemberList.None).ReverseMap();
            CreateMap<Position, PositionDTO>(MemberList.None).ReverseMap();
            CreateMap<Employee, EmployeeDTO>(MemberList.None).ReverseMap();
        }
    }
}
