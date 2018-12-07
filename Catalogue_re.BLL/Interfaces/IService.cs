using System.Collections.Generic;

namespace Catalogue_re.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int? id);
        void Add(T item);
        void Update(T item);
        void Delete(int? id);
        void Dispose();
    }
}
