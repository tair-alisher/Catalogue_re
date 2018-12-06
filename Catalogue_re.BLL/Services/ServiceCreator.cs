using Catalogue_re.BLL.Interfaces;
using Catalogue_re.DAL.Repositories;

namespace Catalogue_re.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
    }
}
