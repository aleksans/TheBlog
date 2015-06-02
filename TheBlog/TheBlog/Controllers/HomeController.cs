using System.Linq;
using System.Web.Mvc;
using TheBlog.Service.Interfaces;

namespace TheBlog.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;

        public HomeController(IPostService postService, ITagService tagService, ICategoryService categoryService)
        {
            _postService = postService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //var role = new Role { Name = "Administrator" };

            //var category = new List<Category> { new Category { Name = "FrontEnd" }, new Category { Name = "Test" } };

            //var tags = new List<Tag> { new Tag { Name = "Azure" }, new Tag { Name = "AWS" } };
            //var user = new User
            //{
            //    Role = role,
            //    DateCreated = DateTime.Now,
            //    Email = "temp",
            //    Username = "serhii.A"
            //};

            //var posts = new List<Post>{new Post
            //{
            //    User = user, Category = category.First(), ShortDescription = "hop hei lalalei", Title = "Some Test Descriptiom",
            //    AddedOn = DateTime.Now, Tags = tags, FullDescription = "AnotherTest"
            //}};

            //_blogContext.Posts.Add(posts.First());
            //_blogContext.SaveChanges();

            var anythyng = _postService.GetAll().OrderByDescending(x => x.AddedOn).ToList();
            ViewBag.Tags = _tagService.GetAll().Select(x => x.Name).ToList();
            ViewBag.Categories = _categoryService.GetAll().Select(x => x.Name).ToList();
            //var result = _postRepository.GetAll();
            //var newpost = new Post
            //{
            //    FullDescription = "FullDescription1",
            //    ShortDescription = "ShortDescription1",
            //    Title = "Title"
            //};
            //_blogContext.Posts.Add(newpost);
            //_blogContext.SaveChanges();
            // _postRepository.Add(newpost);
            //result = _postRepository.GetAll();

            return View(anythyng);
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}