using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using TheBlog.Model;

namespace TheBlog.DAL.Interfaces
{
    public interface IBlogContext
    {
        DbSet<Post> Posts { get; set; }

        DbSet<Tag> Tags { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Role> Roles { get; set; }

        int SaveChanges();
    }
}
