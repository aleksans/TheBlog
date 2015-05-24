using System.Data.Entity.ModelConfiguration;
using TheBlog.Model;

namespace TheBlog.DAL.Mappings
{
    public class PostsMap : EntityTypeConfiguration<Post>
    {
        public PostsMap()
        {
            HasKey(k => k.PostId);
        }
    }
}
