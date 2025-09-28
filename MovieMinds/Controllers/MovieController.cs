using Microsoft.AspNetCore.Mvc;

namespace MovieMinds.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
