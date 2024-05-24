using Microsoft.AspNetCore.Mvc;
using UMS.Quiz.BusinessLayers;
using UMS.Quiz.DomainModels;
using UMS.Quiz.Web.Codes;
using UMS.Quiz.Web.Constants;
using UMS.Quiz.Web.Models;

namespace UMS.Quiz.Web.Controllers
{
/// <summary>
/// Cấu trúc đề
/// </summary>
    public class TopicTemplatesController : Controller
    {
        const int PAGE_SIZE = 0;
        const string CREATE_TITLE = "Tạo cấu trúc đề mới";
        const string TOPICTEMPLATE_SEARCH = "TopicTemplate_search"; //Tên biến session dùng để lưu lại điều kiện tìm kiếm
        public IActionResult Index()
        {
            ViewBag.Title = "Quản lý cấu trúc đề thi";

            TopicTemplateInput? input = ApplicationContext.GetSessionData<TopicTemplateInput>(TOPICTEMPLATE_SEARCH);
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
                input = new TopicTemplateInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    TermID = accountDB!.TermId! ?? "",
                    ExamTime = ExamTimeContant.DEFAULT,
                };
            }
            return View(input);
        }
        public IActionResult Search(TopicTemplateInput input)
        {
            int rowCount = 0;

            var accountId = int.Parse((HttpContext.User.Claims.FirstOrDefault())!.Value);
            //var AnswerText = ((HttpContent.claims.FirstOrDefault())!.Value);
            var data = CommonDataService.ListOfTopicTemplate(
                out rowCount,
                input.Page,
                input.PageSize,
                input.SearchValue ?? "",
                input.TermID,
                input.KnowledgeId,
                accountId,
                input.ExamTime
            );

            var model = new TopicTemplateSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                TermID = input.TermID,
                
                RowCount = rowCount,
                AccountId = accountId,
                KnowledgeId = input.KnowledgeId,
                Data = data,
                
            };
            // Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(TOPICTEMPLATE_SEARCH, input);
            return View(model);
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
