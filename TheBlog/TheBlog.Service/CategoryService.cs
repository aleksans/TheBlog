using System.Collections.Generic;
using TheBlog.DAL.Interfaces;
using TheBlog.Model;
using TheBlog.Service.Interfaces;

namespace TheBlog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _category;

        public CategoryService(IRepository<Category>  category)
        {
            _category = category;
        }

        public IEnumerable<Category> GetAll()
        {
            return _category.GetAll();
        }
    }
}
