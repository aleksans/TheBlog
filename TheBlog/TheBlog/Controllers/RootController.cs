using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheBlog.DAL.Interfaces;
using TheBlog.Model;

namespace TheBlog.Controllers
{
    public class RootController : Controller
    {
        private readonly IBlogContext _context;

        public RootController(IBlogContext blogContext)
        {
            _context = blogContext;
        }

        // GET: Root
        public ActionResult Index()
        {
            return View("~/Views/Home/Root.cshtml");
        }

        public ContentResult Posts(GridModel model)
        {
            
            var posts = _context.Posts.ToList();

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
            var postsCount = _context.Posts.Count();
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
                _context.Posts.Add(post);
                _context.SaveChanges();

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

        [HttpPost]
        public ContentResult EditPost(Post post)
        {
            string json;

            if (ModelState.IsValid)
            {
                _context.Posts.Attach(post);
                _context.SaveChanges();

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
            var categories = _context.Categories.OrderBy(s => s.Name);

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
            var tags = _context.Tags.OrderBy(s => s.Name);

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
    }
}