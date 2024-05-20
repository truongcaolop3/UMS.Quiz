using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using UMS.Quiz.BusinessLayers;
using UMS.Quiz.DataLayers.SQLServer;
using UMS.Quiz.DomainModels;
using UMS.Quiz.Web.Codes;
using UMS.Quiz.Web.Data;
using UMS.Quiz.Web.Models;

namespace UMS.Quiz.Web.Controllers
{
    /// <summary>
    /// khối kiến thức
    /// </summary>
    [Authorize]
    public class KnowledgesController : Controller
    {
        const int PAGE_SIZE = 12;
        const string CREATE_TITLE = "Tạo khối kiến thức mới";
        const string KNOWLEDGES_SEARCH = "Knowledges_search"; //Tên biến session dùng để lưu lại điều kiện tìm kiếm

            

        public IActionResult Index()
        {
            //Kiểm tra xem trong session có lưu điều kiện tìm kiếm không.
            //Nếu có thì sử dụng lại điều kiện tìm kiếm, ngược lại thì tìm kiếm theo điều kiện mặc định

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
            //PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(KNOWLEDGES_SEARCH);
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            ////var accountDB = CommonDataService.GetAccount(int.Parse(accountId!.Value));
            //if (input == null)
            //{
            //    input = new PaginationSearchInput()
            //    {
            //        Page = 1,
            //        PageSize = PAGE_SIZE,
            //        SearchValue = "",
            //        TermID = CommonDataService.GetAccount(userId)?.TermId ?? "",
            //    };
            //}
            //return View(input);
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
                AccountId = accountId,
                Data = data,
            };
            // Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(KNOWLEDGES_SEARCH, input);

            return View(model);
            //int rowCount = 0;
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            //var data = CommonDataService.ListOfKnowledges(
            //    out rowCount,
            //    input.Page,
            //    input.PageSize,
            //    input.SearchValue ?? "",
            //    input.TermID
            //).Where(k => k.AccountId == userId).ToList(); // Chỉ lấy những khối kiến thức của người dùng hiện tại

            //var model = new KnowledgesSearchResult()
            //{
            //    Page = input.Page,
            //    PageSize = input.PageSize,
            //    SearchValue = input.SearchValue ?? "",
            //    TermID = input.TermID,
            //    RowCount = rowCount,
            //    Data = data,
            //};

            //// Lưu lại điều kiện tìm kiếm
            //ApplicationContext.SetSessionData(KNOWLEDGES_SEARCH, input);

            //return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Title = CREATE_TITLE;
            var model = new Knowledges()
            {
                KnowledgeId = 0
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Sửa khối kiến thức";
            var model = CommonDataService.GetKnowledges(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Save(Knowledges model)
        {
            //TODO: Kiểm soát dữ liệu trong model xem có hợp lệ hay không?
            //Yêu cầu: Tên khách hàng, tên giao dịch, Email và tỉnh thành không được để trống
            if (string.IsNullOrWhiteSpace(model.KnowledgeName))
                ModelState.AddModelError("KnowledgeName", "Tên khối kiến thức không được trống");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.KnowledgeId == 0 ? CREATE_TITLE : "Cập nhật khối kiến thức";
                return View("Edit", model);
            }
            if (model.KnowledgeId == 0)
            {
                //int termid = CommonDataService.AddTerm(Terms.);
                //int ID = CommonDataService.AddQuizQuestion(modelquizQuestion);
                var accountId = int.Parse((HttpContext.User.Claims.FirstOrDefault())!.Value);
                model.AccountId = accountId;

                int id = CommonDataService.AddKnowledges(model);
                if (id <= 0)    
                {
                    ModelState.AddModelError("KnowledgesName", "Tên khối kiến thức bị trùng");
                    ViewBag.Title = CREATE_TITLE;
                    return View("Edit", model);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateKnowledges(model);
                if (!result)
                {
                    ModelState.AddModelError("Error", "Không cập nhật được khối kiến thức. Có thể bị trùng tên khối kiến thức.");
                    ViewBag.Title = "Cập nhật khối kiến thức";
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            ViewBag.Title = "Xóa khối kiến thức";
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
