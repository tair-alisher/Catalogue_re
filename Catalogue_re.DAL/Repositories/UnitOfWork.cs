using CatalogEntities;
using Catalogue_re.DAL.EF;
using Catalogue_re.DAL.Entities;
using Catalogue_re.DAL.Identity;
using Catalogue_re.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Catalogue_re.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatalogueContext context;

        private BaseRepository<Division> divisionRepository;
        private BaseRepository<Administration> administrationRepository;
        private BaseRepository<Department> departmentRepository;
        private BaseRepository<Position> positionRepository;
        private BaseRepository<Employee> employeeRepository;

        PasswordValidator passwordValidator = new PasswordValidator
        {
            RequiredLength = 8,
            RequireNonLetterOrDigit = true,
            RequireDigit = true,
            RequireLowercase = true
        };

        public UnitOfWork(string connectionString)
        {
            context = new CatalogueContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            UserManager.PasswordValidator = passwordValidator;
            UserManager.UserValidator = new AppUserValidator(UserManager);
        }

        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }

        public IRepository<Division> Divisions
        {
            get
            {
                if (divisionRepository == null)
                    divisionRepository = new BaseRepository<Division>(context);
                return divisionRepository;
            }
        }

        public IRepository<Administration> Administrations
        {
            get
            {
                if (administrationRepository == null)
                    administrationRepository = new BaseRepository<Administration>(context);
                return administrationRepository;
            }
        }

        public IRepository<Department> Departments
        {
            get
            {
                if (departmentRepository == null)
                    departmentRepository = new BaseRepository<Department>(context);
                return departmentRepository;
            }
        }

        public IRepository<Position> Positions
        {
            get
            {
                if (positionRepository == null)
                    positionRepository = new BaseRepository<Position>(context);
                return positionRepository;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new BaseRepository<Employee>(context);
                return employeeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
