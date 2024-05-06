using Microsoft.AspNetCore.Mvc;

namespace UMS.Quiz.Web.Controllers
{
/// <summary>
/// Cấu trúc đề
/// </summary>
    public class TopicTemplatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Tạo Cấu trúc đề";
            return View();
        }
        public IActionResult Edit()
        {
            ViewBag.Title = "Tạo bộ đề thi trắc nghiệm theo cấu trúc";
            return View();
        }
        public IActionResult Delete()
        {
            ViewBag.Title = "Xóa cấu trúc đề";
            return View();
        }
    }
}
