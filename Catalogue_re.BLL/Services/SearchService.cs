using System.Collections.Generic;
using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.DAL.Interfaces;

namespace Catalogue_re.BLL.Services
{
    public class SearchService : ISearchService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public SearchService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<EmployeeDTO> GetFilteredEmployeeList(FilterParamsDTO parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
