using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UMS.Quiz.BusinessLayers;
using UMS.Quiz.Web.Codes;
using UMS.Quiz.Web.Models;

namespace UMS.Quiz.Web.Controllers
{
    /// <summary>
    /// Câu hỏi trắc nghiệm
    /// </summary>
    [Authorize]
    public class QuizzesController : Controller
    {
        const int PAGE_SIZE = 12;
        //const string CREATE_TITLE = "Tạo khối kiến thức mới";
        const string KNOWLEDGES_SEARCH = "Knowledges_search"; //Tên biến session dùng để lưu lại điều kiện tìm kiếm

        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý thư viện trắc nghiệm";
            //Kiểm tra xem trong session có lưu điều kiện tìm kiếm không.
            //Nếu có thì sử dụng lại điều kiện tìm kiếm, ngược lại thì tìm kiếm theo điều kiện mặc địnhx    
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(KNOWLEDGES_SEARCH);
            var accountId = HttpContext.User.Claims.FirstOrDefault();
            var accountDB = CommonDataService.GetAccount(int.Parse(accountId!.Value));
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    //TermID = CommonDataService.GetAccount(Account.AccountId)?.TermId ?? "",
                    TermID = accountDB!.TermId! ?? "",
                    //AccountID = accountId!.Value,
                };
            }
            return View(input);
            
        }
        
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var accountId = int.Parse((HttpContext.User.Claims.FirstOrDefault())!.Value);
            var data = CommonDataService.ListOfKnowledges(
                out rowCount,
                input.Page,
                input.PageSize,
                input.SearchValue ?? "",
                input.TermID,
                accountId
            );

            var model = new KnowledgesSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                TermID = input.TermID,
                RowCount = rowCount,
                Data = data,
            };

            // Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(KNOWLEDGES_SEARCH, input);

            return View(model);
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
