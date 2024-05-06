using Microsoft.AspNetCore.Mvc;

namespace UMS.Quiz.Web.Controllers
{
    /// <summary>
    /// Danh sách sinh viên
    /// </summary>
    public class ExamDetailStudent : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý danh sách sinh viên";
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.Title = "Quản lý danh sách sinh viên";
            return View();
        }
        public IActionResult Edit()
        {
            ViewBag.Title = "Quản lý danh sách sinh viên";
            return View();
        }
        public IActionResult Delete()
        {
            ViewBag.Title = "Quản lý danh sách sinh viên";
            return View();
        }
        public IActionResult DeleteAll()
        {
            ViewBag.Title = "Quản lý danh sách sinh viên";
            return View();
        }
        public IActionResult DetailAnswer()
        {
            ViewBag.Title = "Chi tiết câu trả lời ";
            return View();
        }
    }
}
