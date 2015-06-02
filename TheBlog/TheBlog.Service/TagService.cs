using System.Collections.Generic;
using System.Linq;
using TheBlog.DAL.Interfaces;
using TheBlog.Model;
using TheBlog.Service.Interfaces;

namespace TheBlog.Service
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepository;

        public TagService(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll().ToList();
        }
    }
}
