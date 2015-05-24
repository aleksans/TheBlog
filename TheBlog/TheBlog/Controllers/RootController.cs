using System.Web.Mvc;

namespace TheBlog.Controllers
{
    public class RootController : Controller
    {
        // GET: Root
        public ActionResult Index()
        {
            return View("~/Views/Home/Root.cshtml");
        }
    }
}