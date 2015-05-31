using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheBlog.Model
{
    public sealed class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }
    }
}
