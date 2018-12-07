using Catalogue_re.DAL.Interfaces;
using Catalogue_re.DAL.Repositories;
using Ninject.Modules;

namespace Catalogue_re.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>()
                .WithConstructorArgument(connectionString);
        }
    }
}
