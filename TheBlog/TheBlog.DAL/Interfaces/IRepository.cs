using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TheBlog.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void SaveChanges();
        Int32 Count(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long Id);
        T GetById(string Id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        List<T> GetMany(Expression<Func<T, bool>> where, string includeProperties = "");

        int GetCount(Expression<Func<T, bool>> where);
    }
}
