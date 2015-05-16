using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TheBlog.DAL.Interfaces;

namespace TheBlog.DAL
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private BlogContext _dataContext;
        private readonly IDbSet<T> _dbSet;

        protected Repository(IDatabaseFactory databaseFactory)
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

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
