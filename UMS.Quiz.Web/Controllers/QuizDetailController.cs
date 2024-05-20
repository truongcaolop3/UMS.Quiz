using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.Quiz.BusinessLayers;
using UMS.Quiz.DomainModels;
using UMS.Quiz.Web.Codes;
using UMS.Quiz.Web.Constants;
using UMS.Quiz.Web.Models;

namespace UMS.Quiz.Web.Controllers
{
    [Authorize]
    public class QuizDetailController : Controller
    {
        const int PAGE_SIZE = 6;
        const string CREATE_TITLE = "Tạo câu hỏi mới";
        const string QUESTIONDETAIL_SEARCH = "QuestionDetail_search"; //Tên biến session dùng để lưu lại điều kiện tìm kiếm
        public IActionResult Index(int knowledgeId = 0)
        {
            ViewBag.Title = "Quản lý Chi tiết thư viện trắc nghiệm";
            QuestionDetailInput? input = ApplicationContext.GetSessionData<QuestionDetailInput>(QUESTIONDETAIL_SEARCH);
            // Lấy accountId từ claims
            var accountIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "AccountId");
            if (accountIdClaim == null)
            {
                // Xử lý trường hợp không tìm thấy claim
                return Unauthorized();
            }

            var accountDB = CommonDataService.GetAccount(int.Parse(accountIdClaim.Value));
            if (accountDB == null)
            {
                // Xử lý trường hợp không tìm thấy account
                return Unauthorized();
            }

            if (input == null)
            {
                input = new QuestionDetailInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    QuestionType = QuestionTypeConstant.DEFAULT,
                    KnowledgeId = knowledgeId,
                };
            }
            return View(input);
        }
        public IActionResult Search(QuestionDetailInput input)
        {
            int rowCount = 0;

            var accountId = int.Parse((HttpContext.User.Claims.FirstOrDefault())!.Value);
            var data = CommonDataService.ListOfQuestionDetail(
                out rowCount,
                input.Page,
                input.PageSize,
                input.SearchValue ?? "",
                input.QuestionType,
                input.KnowledgeId,
                accountId
            );

            var model = new QuestionDetailSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data

            };

            // Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(QUESTIONDETAIL_SEARCH, input);

            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Title = CREATE_TITLE;
            var model = new QuestionDetail()
            {
                QuestionDetailID = 0
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Sửa câu hỏi trắc nghiệm";
            var model = CommonDataService.GetQuestionDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Save(QuestionDetail model)
        {
            //TODO: Kiểm soát dữ liệu trong model xem có hợp lệ hay không?
            //Yêu cầu: Tên khách hàng, tên giao dịch, Email và tỉnh thành không được để trống
            if (string.IsNullOrWhiteSpace(model.QuestionText))
                ModelState.AddModelError("QuestionText", "Tên khối kiến thức không được trống");
            
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.KnowledgeId == 0 ? CREATE_TITLE : "Cập nhật câu hỏi trắc nghiệm";
                return View("Edit", model);
            }
            if (model.QuestionDetailID == 0)
            {
                //int termid = CommonDataService.AddTerm(Terms.);
                //int ID = CommonDataService.AddQuizQuestion(modelquizQuestion);
                var accountId = int.Parse((HttpContext.User.Claims.FirstOrDefault())!.Value);
                model.AccountId = accountId;

                int id = CommonDataService.AddQuestionDetail(model);
                if (id <= 0)
                {
                    ViewBag.Title = CREATE_TITLE;
                    return View("Edit", model);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateQuestionDetail(model);
                if (!result)
                {
                    ModelState.AddModelError("Error", "Không cập nhật được câu hỏi trắc nghiệm");
                    ViewBag.Title = "Cập nhật khối kiến thức";
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa câu hỏi trắc nghiệm";
            if (Request.Method == "POST")
            {
                bool result = CommonDataService.DeleteKnowledges(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetKnowledges(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}
