using CatalogEntities;
using Catalogue_re.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Catalogue_re.DAL.EF
{
    public class CatalogueContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Administration> Administrations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        static CatalogueContext() { Database.SetInitializer<CatalogueContext>(null); }

        public CatalogueContext(string connectionString) : base(connectionString) { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Division>()
        //        .HasMany<Administration>(d => d.Administrations)
        //        .WithRequired(a => a.Division)
        //        .HasForeignKey<int>(a => a.DivisionId);
        //    modelBuilder.Entity<Administration>()
        //        .HasMany<Department>(a => a.Departments)
        //        .WithRequired(d => d.Administration)
        //        .HasForeignKey<int>(d => d.AdministrationId);
        //    modelBuilder.Entity<Department>()
        //        .HasMany<Employee>(d => d.Employees)
        //        .WithRequired(e => e.Department)
        //        .HasForeignKey<int>(e => e.DepartmentId);
        //    modelBuilder.Entity<Position>()
        //        .HasMany<Employee>(p => p.Employees)
        //        .WithRequired(e => e.Position)
        //        .HasForeignKey<int>(e => e.PositionId);
        //}
    }
}
