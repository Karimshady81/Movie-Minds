using Microsoft.AspNetCore.Mvc;

namespace MovieMinds.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
