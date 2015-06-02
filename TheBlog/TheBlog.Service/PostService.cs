using System.Collections.Generic;
using TheBlog.DAL.Interfaces;
using TheBlog.Model;
using TheBlog.Service.Interfaces;

namespace TheBlog.Service
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;

        public PostService(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public Post GetPostById(int id)
        {
            return _postRepository.GetById(id);
        }

        public int Count()
        {
            return _postRepository.Count();
        }

        public void AddPost(Post post)
        {
            _postRepository.Add(post);
            _postRepository.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _postRepository.Update(post);
            _postRepository.SaveChanges();
        }

        public bool DeletePost(int id)
        {
            var post = GetPostById(id);
            if (post != null)
            {
                _postRepository.Delete(post);
                _postRepository.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
