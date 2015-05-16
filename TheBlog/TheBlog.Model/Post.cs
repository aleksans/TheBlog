using System.ComponentModel.DataAnnotations;

namespace TheBlog.Model
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }
    }
}
