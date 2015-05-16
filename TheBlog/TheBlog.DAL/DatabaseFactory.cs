using TheBlog.DAL.Interfaces;

namespace TheBlog.DAL
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IBlogContext _blogContext;

        public DatabaseFactory(IBlogContext context)
        {
            _blogContext = context;
        }


        public BlogContext Get()
        {
            return (BlogContext) _blogContext;
        }
    }
}
