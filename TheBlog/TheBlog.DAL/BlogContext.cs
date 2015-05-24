﻿using System.Data.Entity;
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

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }

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
