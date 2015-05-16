using System.Collections.Generic;

namespace TheBlog.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Delete(T entity);
    }
}
