using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TheBlog.Model;

namespace TheBlog.DAL.Interfaces
{
    public interface IBlogContext : IDisposable
    {
        IDbSet<Post> Posts { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Role> Roles { get; set; }

        int SaveChanges();

        DbEntityEntry Entry(object entity);
    }
}
