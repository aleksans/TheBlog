using System.Linq;
using System.Web.Mvc;
using TheBlog.DAL.Interfaces;
using TheBlog.Repository.Interfaces;

namespace TheBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogContext _blogContext;


        public HomeController(IBlogContext blogContext)
        {
            //_postRepository = postRepository;
            _blogContext = blogContext;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var anythyng = _blogContext.Posts.ToList();

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