using TheBlog.DAL;
using TheBlog.DAL.Interfaces;
using TheBlog.Model;
using TheBlog.Repository.Interfaces;

namespace TheBlog.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private BlogContext dataContext;

        protected new IDatabaseFactory DatabaseFactory { get; private set; }

        public PostRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        protected new BlogContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
    }
}
