using System.Data.Entity;
using TheBlog.Model;

namespace TheBlog.DAL.Interfaces
{
    public interface IBlogContext
    {
        IDbSet<Post> Posts { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Role> Roles { get; set; }

        int SaveChanges();
    }
}
