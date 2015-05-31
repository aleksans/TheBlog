using System.Data.Entity;
using TheBlog.DAL.Interfaces;
using TheBlog.DAL.Mappings;
using TheBlog.Model;

namespace TheBlog.DAL
{
    public class BlogContext : DbContext, IBlogContext
    {
        public BlogContext()
            : base("DbConnection")
        {
        }

        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Role> Roles { get; set; }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, DAL.Migrations.Configuration>());
            modelBuilder.Configurations.Add(new PostsMap());
        }
    }
}
