using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheBlog.Model
{
    public class Post
    {
        public Post()
        {
            Tags = new HashSet<Tag>();
        }

        public int PostId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public DateTime AddedOn { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
