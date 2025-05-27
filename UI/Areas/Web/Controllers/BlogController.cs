using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Web.Controllers
{
    [Area("Web")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
