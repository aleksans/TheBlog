using System.Collections.Generic;

namespace TheBlog.Model
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
