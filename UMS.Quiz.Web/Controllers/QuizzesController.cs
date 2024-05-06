﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        const int PAGE_SIZE = 2;
        const string CREATE_TITLE = "Tạo khối kiến thức mới";
        const string QUIZQUESTION_SEARCH = "Knowledges_search"; //Tên biến session dùng để lưu lại điều kiện tìm kiếm

        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý thư viện trắc nghiệm";
            //Kiểm tra xem trong session có lưu điều kiện tìm kiếm không.
            //Nếu có thì sử dụng lại điều kiện tìm kiếm, ngược lại thì tìm kiếm theo điều kiện mặc định
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(QUIZQUESTION_SEARCH);
            var accountId = HttpContext.User.Claims.FirstOrDefault();
            var accountDB = CommonDataService.GetAccount(int.Parse(accountId!.Value));
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    TermID = accountDB!.TermId! ?? "",
                };
            }
            return View(input);
        }
        
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfQuizQuestion(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "", input.TermID);

            var model = new QuizQuestionSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                TermID = input.TermID,
                RowCount = rowCount,
                Data = data,
            };

            // Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(QUIZQUESTION_SEARCH, input);

            return View(model);
            //return PartialView("_SearchResult", model);
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
