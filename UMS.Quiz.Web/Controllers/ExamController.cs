using Microsoft.AspNetCore.Mvc;

namespace UMS.Quiz.Web.Controllers
{
    /// <summary>
    /// đề thi
    /// </summary>
    public class ExamController : Controller
    {
        
        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý bộ đề thi";
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Tạo Đề thi";
            return View();
        }
        public IActionResult DetailExam()
        {
            ViewBag.Title = "Chi tiết bộ đề thi";
            return View();
        }
        public IActionResult DetailExamAnswer()
        {
            ViewBag.Title = "Nội dung và đáp án của đề thi";
            return View();
        }
        public IActionResult Delete()
        {
            ViewBag.Title = "Xóa Đề thi";
            return View();
        }
        public IActionResult Status()
        {
            return View();
        }
    }
}
