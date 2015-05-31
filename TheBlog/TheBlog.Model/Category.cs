using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheBlog.Model
{
    public class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
