using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheBlog.Model;
using TheBlog.Service.Interfaces;

namespace TheBlog.Controllers
{
    public class RootController : Controller
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;

        public RootController(IPostService postService, ITagService tagService, ICategoryService categoryService)
        {
            _postService = postService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        // GET: Root
        public ActionResult Index()
        {
            return View("~/Views/Home/Root.cshtml");
        }

        public ContentResult Posts(GridModel model)
        {

            var posts = _postService.GetAll().ToList();

            //if (model.sidx != null)
            //{
            //    model.page = 1;
            //}
            //else
            //{
            //    //model. = currentFilter;
            //}

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    posts = posts.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
            //}

            switch (model.sord)
            {
                case "desc":
                    posts = posts.OrderByDescending(x => x.PostId).ToList();
                    break;
                default:
                    posts = posts.OrderBy(x => x.PostId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = model.page -1;

            posts = posts.Skip((pageNumber * pageSize)).Take(pageSize).ToList();
            var postsCount = _postService.Count();
            var content = Content(JsonConvert.SerializeObject(new
            {
                page = pageNumber,
                records = postsCount,
                rows = posts,
                total = Math.Ceiling(Convert.ToDouble(postsCount) / model.rows)
            }), "application/json");

            return content;
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult AddPost(Post post)
        {
            string json;
            ModelState.Clear();
            
            if (TryValidateModel(post))
            {
                _postService.AddPost(post);

                json = JsonConvert.SerializeObject(new
                {
                    id = post.PostId,
                    success = true,
                    message = "Post added successfully"
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post"
                });
            }

            return Content(json, "application/json");
        }

         [HttpPost, ValidateInput(false)]
        public ContentResult EditPost(Post post)
        {
            string json;
            ModelState.Clear();

            if (TryValidateModel(post))
            {
                _postService.UpdatePost(post);

                json = JsonConvert.SerializeObject(new
                {
                    id = post.PostId,
                    success = true,
                    message = "Post added successfully"
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post"
                });
            }

            return Content(json, "application/json");
        }

        public ContentResult GetCategoriesHtml()
        {
            var categories = _categoryService.GetAll().OrderBy(s => s.Name);

            var sb = new StringBuilder();
            sb.AppendLine(@"<select>");

            foreach (var category in categories)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    category.CategoryId, category.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        public ContentResult GetTagsHtml()
        {
            var tags = _tagService.GetAll().OrderBy(s => s.Name);

            var sb = new StringBuilder();
            sb.AppendLine(@"<select multiple=""multiple"">");

            foreach (var tag in tags)
            {
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>",
                    tag.Id, tag.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        [HttpPost]
        public ContentResult DeletePost(int id)
        {
            string json;
            var result = _postService.DeletePost(id);
            if (result)
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = true,
                    message = "Post deleted successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = true,
                    message = "Post not found."
                });
            }

            return Content(json, "application/json");
        }
    }
}