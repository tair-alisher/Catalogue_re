using AutoMapper;
using Catalogue_re.BLL.DTO;
using Catalogue_re.Web.Models.ViewModels;

namespace Catalogue_re.Web.MappingProfiles
{
    public class WebMappingProfile : Profile
    {
        public  WebMappingProfile()
        {
            CreateMap<DivisionDTO, DivisionVM>(MemberList.None).ReverseMap();
            CreateMap<AdministrationDTO, AdministrationVM>(MemberList.None).ReverseMap();
            CreateMap<DepartmentDTO, DepartmentVM> (MemberList.None).ReverseMap();
            CreateMap<PositionDTO, PositionVM>(MemberList.None).ReverseMap();
            CreateMap<EmployeeDTO, EmployeeVM>(MemberList.None).ReverseMap();
        }
    }
}