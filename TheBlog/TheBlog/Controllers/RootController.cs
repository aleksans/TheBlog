using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheBlog.DAL.Interfaces;
using TheBlog.Model;

namespace TheBlog.Controllers
{
    public class RootController : Controller
    {
        private IBlogContext _context;

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

        [HttpPost]
        public ContentResult AddPost(Post post)
        {
            string json;

            if (ModelState.IsValid)
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
    }
}