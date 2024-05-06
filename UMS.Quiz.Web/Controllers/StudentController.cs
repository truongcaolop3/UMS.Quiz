using Microsoft.AspNetCore.Mvc;

namespace UMS.Quiz.Web.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.title = "Thi trắc nghiệm";
            return View();
        }
        public IActionResult Submit()
        {
            return View();
            //return View("LoginStudent");

        }
    }
}
