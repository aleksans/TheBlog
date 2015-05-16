using System.Data.Entity;
using TheBlog.Model;

namespace TheBlog.DAL.Interfaces
{
    public interface IBlogContext
    {
        DbSet<Post> Posts { get; set; }

        int SaveChanges();
    }
}
