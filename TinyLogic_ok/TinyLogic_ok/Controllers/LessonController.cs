using Microsoft.AspNetCore.Mvc;

namespace TinyLogic_ok.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
