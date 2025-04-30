using Microsoft.AspNetCore.Mvc;

namespace LinuxExamAPI.Controllers
{

    [Route("home")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Title = "Linux Exam API";

            return View();
        }
    }
}
