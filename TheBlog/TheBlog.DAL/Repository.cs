using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TheBlog.DAL.Interfaces;

namespace TheBlog.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private BlogContext _dataContext;
        private readonly IDbSet<T> _dbSet;

        public Repository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbSet = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get; private set;
        }

        protected BlogContext DataContext
        {
            get { return _dataContext ?? DatabaseFactory.Get(); }
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        public Int32 Count(Expression<Func<T, bool>> predicate = null)
        {
            Int32 count = 0;

            if (predicate != null)
            {
                count = _dbSet.Count(predicate);
            }
            else
            {
                count = _dbSet.Count();
            }

            return count; ;
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return _dbSet.Find(id);
        }
        public virtual T GetById(string id)
        {
            return _dbSet.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual List<T> GetMany(Expression<Func<T, bool>> where, string includeProperty = "")
        {
            if (string.IsNullOrWhiteSpace(includeProperty))
            {
                return _dbSet.Where(where).ToList();
            }
            return _dbSet.Where(where).Include(includeProperty).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual int GetCount(Expression<Func<T, bool>> where)
        {
            return _dbSet.Count(where);
        }
    }
}
