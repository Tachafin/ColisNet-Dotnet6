using Microsoft.AspNetCore.Mvc;

namespace _1311.Controllers
{
    public class AccueilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
