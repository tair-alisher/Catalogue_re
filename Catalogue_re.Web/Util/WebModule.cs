using Catalogue_re.BLL.Interfaces;
using Catalogue_re.BLL.Services;
using Ninject.Modules;

namespace Catalogue_re.Web.Util
{
    public class WebModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDivisionService>().To<DivisionService>();
            Bind<IAdministrationService>().To<AdministrationService>();
            Bind<IDepartmentService>().To<DepartmentService>();
            Bind<IPositionService>().To<PositionService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<ISearchService>().To<SearchService>();
        }
    }
}