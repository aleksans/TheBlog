using System.Collections.Generic;
using TheBlog.Model;

namespace TheBlog.Service.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        Post GetPostById(int id);
        int Count();
        void AddPost(Post post);
        void UpdatePost(Post post);
        bool DeletePost(int id);
    }
}