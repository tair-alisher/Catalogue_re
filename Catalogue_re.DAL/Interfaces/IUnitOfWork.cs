using CatalogEntities;
using Catalogue_re.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace Catalogue_re.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IRepository<Division> Divisions { get; }
        IRepository<Administration> Administrations { get; }
        IRepository<Department> Departments { get; }
        IRepository<Position> Positions { get; }
        IRepository<Employee> Employees { get; }
        Task SaveAsync();
        void Save();
    }
}
