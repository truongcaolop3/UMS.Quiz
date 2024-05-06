using Microsoft.AspNetCore.Mvc;

namespace UMS.Quiz.Web.Controllers
{
    /// <summary>
    /// tổ chức thi
    /// </summary>
    public class ExamCandidatesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý tổ chức thi";
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Thêm mới cuộc thi";
            return View("Edit");
        }
        public IActionResult Edit()
        {
            ViewBag.Title = "Sửa cuộc thi ";
            return View();
        }
        public IActionResult Delete()
        {
            ViewBag.Title = "Xóa cuộc thi";
            return View();
        }
    }
}
