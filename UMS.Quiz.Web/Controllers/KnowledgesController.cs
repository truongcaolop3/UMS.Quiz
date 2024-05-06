using Microsoft.AspNetCore.Mvc;

namespace UMS.Quiz.Web.Controllers
{
    /// <summary>
    /// khối kiến thức
    /// </summary>
    public class KnowledgesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Tạo khối kiến thức";
            return View("Edit");
        }
        public IActionResult Edit()
        {
            ViewBag.Title = "Sửa khối kiến thức";
            return View();
        }
        public IActionResult Delete()
        {
            ViewBag.Title = "Xóa khối kiến thức";
            return View();
        }
    }
}
