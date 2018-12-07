using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Catalogue_re.DAL.EF;
using Catalogue_re.DAL.Interfaces;

namespace Catalogue_re.DAL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private CatalogueContext Context;
        private DbSet<T> DbSet;

        public BaseRepository(CatalogueContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public void Create(T item)
        {
            DbSet.Add(item);
        }

        public void Delete(int id)
        {
            T item = DbSet.Find(id);
            if (item != null)
                DbSet.Remove(item);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public T Get(int? id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public void Update(T item)
        {
            var entry = Context.Entry(item);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(item);
            entry.State = EntityState.Modified;
        }
    }
}
