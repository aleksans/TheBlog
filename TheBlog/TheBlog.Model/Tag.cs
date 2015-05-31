using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheBlog.Model
{
    public class Tag
    {
        public Tag()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
