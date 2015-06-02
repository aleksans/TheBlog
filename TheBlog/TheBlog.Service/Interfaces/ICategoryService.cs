using System.Collections.Generic;
using TheBlog.Model;

namespace TheBlog.Service.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
    }
}