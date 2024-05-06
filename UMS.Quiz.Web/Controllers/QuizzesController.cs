using Microsoft.AspNetCore.Mvc;

namespace UMS.Quiz.Web.Controllers
{
    /// <summary>
    /// Câu hỏi trắc nghiệm
    /// </summary>
    public class QuizzesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý thư viện trắc nghiệm";
            return View();
        }
        public IActionResult QuizDetail()
        {
            ViewBag.Title = "Quản lý Chi tiết thư viện trắc nghiệm";
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Tạo mới câu hỏi trắc nghiệm";
            return View();
        }
        public IActionResult Edit()
        {
            ViewBag.Title = "Sửa câu hỏi trắc nghiệm";
            return View();
        }
        public IActionResult Delete()
        {
            ViewBag.Title = "Xóa câu hỏi trắc nghiệm";
            return View();
        }
    }
}
