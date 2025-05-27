using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Web.Controllers
{
    [Area("Web")] // <- Bu attribute şart!
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
