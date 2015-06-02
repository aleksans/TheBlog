using System.Collections.Generic;
using TheBlog.Model;

namespace TheBlog.Service.Interfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();
    }
}