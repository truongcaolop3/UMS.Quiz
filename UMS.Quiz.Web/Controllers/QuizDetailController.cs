using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        const int PAGE_SIZE = 0;
        const string CREATE_TITLE = "Tạo câu hỏi mới";
        const string QUESTIONDETAIL_SEARCH = "QuestionDetail_search"; //Tên biến session dùng để lưu lại điều kiện tìm kiếm
        public IActionResult Index(int knowledgeId = 0)
        {
            ViewBag.Title = "Quản lý Chi tiết thư viện trắc nghiệm";

            if (knowledgeId == 0)
            {
                return NotFound();
            }

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
                    KnowledgeName = CommonDataService.GetKnowledges(knowledgeId)!.KnowledgeName,
                };
            }
            return View(input);
        }
        public IActionResult Search(QuestionDetailInput input)
        {
            int rowCount = 0;

            var accountId = int.Parse((HttpContext.User.Claims.FirstOrDefault())!.Value);
            //var AnswerText = ((HttpContent.claims.FirstOrDefault())!.Value);
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
                KnowledgeId = input.KnowledgeId,
                KnowledgeName = input.KnowledgeName,
                Data = data,
            };

            // Lưu lại điều kiện tìm kiếm
            ApplicationContext.SetSessionData(QUESTIONDETAIL_SEARCH, input);

            return View(model);
        }

        public IActionResult Create(int knowledgeId = 0)
        {
            ViewBag.Title = CREATE_TITLE;

            if (knowledgeId == 0)
            {
                return RedirectToAction("Index");
            }

            var model = new SaveQuizQuestionDetailViewModel()
            {
                QuestionDetailID = 0,
                KnowledgeId = knowledgeId,
                knowledges = CommonDataService.GetKnowledges(knowledgeId),
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id)
        {

            ViewBag.Title = "Sửa câu hỏi trắc nghiệm";
            var questionDetail = CommonDataService.GetQuestionDetail(id);

            if (questionDetail == null)
            {
                return RedirectToAction("Index");
            }

            var model = new SaveQuizQuestionDetailViewModel
            {
                QuestionDetailID = questionDetail.QuestionDetailID,
                knowledges = questionDetail.knowledges,
                QuestionPoint = questionDetail.QuestionPoint,
                AccountId = questionDetail.AccountId,
                QuestionText = questionDetail.QuestionText,
                QuizQuestionAnswers = CommonDataService.GetQuizQuestionAnswerByQuestionDetailId(id),
                KnowledgeId = questionDetail.KnowledgeId,
                QuestionType = questionDetail.QuestionType,
                QuizQuestionAnswersStr = JsonConvert.SerializeObject(questionDetail.QuizQuestionAnswers),
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Save(SaveQuizQuestionDetailViewModel model)
        {
            //TODO: Kiểm soát dữ liệu trong model xem có hợp lệ hay không?
            //Yêu cầu: Tên khách hàng, tên giao dịch, Email và tỉnh thành không được để trống
            if (string.IsNullOrWhiteSpace(model.QuestionText))
                ModelState.AddModelError("QuestionText", "Câu hỏi không được trống");
            if (model.KnowledgeId > 0)
            {
                model.knowledges = CommonDataService.GetKnowledges(model.KnowledgeId);
            }

            if (!string.IsNullOrWhiteSpace(model.QuizQuestionAnswersStr))
            {
                model.QuizQuestionAnswers = JsonConvert.DeserializeObject<IList<QuizQuestionAnswer>>(model.QuizQuestionAnswersStr)!;
            }
            
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.KnowledgeId == 0 ? CREATE_TITLE : "Cập nhật câu hỏi trắc nghiệm";
                return View("Edit", model);
            }

            // thêm mới thông tin chi tiết câu hỏi
            var accountId = int.Parse((HttpContext.User.Claims.FirstOrDefault())!.Value);
            model.AccountId = accountId;

            if (model.QuestionDetailID == 0)
            {
                ViewBag.Title = CREATE_TITLE;
                int newQuestionDetailId = CommonDataService.AddQuestionDetail(model);

                // trường hợp tạo sai
                if (newQuestionDetailId < 1)
                {
                    ModelState.AddModelError("ErrorMessage", "Có sự cố khi tạo thông tin chi tiết câu hỏi");
                    return View("Edit", model);
                }

                // nếu tạo thành công thì mới tạo câu hỏi
                foreach(var quizQuestionAnswer in model.QuizQuestionAnswers)
                {
                    quizQuestionAnswer.AccountId = accountId;
                    quizQuestionAnswer.QuestionDetailId = newQuestionDetailId;

                    var newquizQuestionAnswerId = CommonDataService.AddQuizQuestionAnswer(quizQuestionAnswer);
                    if (newquizQuestionAnswerId < 1)
                    {
                        ModelState.AddModelError("ErrorMessage", "Có sự cố khi tạo thông tin câu trả lời cho câu hỏi");
                        return View("Edit", model);
                    }
                }

                return View("Edit", model);
            }

            // cập nhật thông tin chi tiết câu hỏi
            else
            {
                ViewBag.Title = "Cập nhật khối kiến thức";
                var questionDetailDb = CommonDataService.GetQuestionDetail(model.QuestionDetailID);

                if (questionDetailDb == null)
                {
                    ModelState.AddModelError("ErrorMessage", "Không tìm thấy thông tin chi tiết của câu hỏi");
                    return View("Edit", model);
                }

                bool result = CommonDataService.UpdateQuestionDetail(model);
                if (!result)
                {
                    ModelState.AddModelError("ErrorMessage", "Không cập nhật được câu hỏi trắc nghiệm");
                    return View("Edit", model);
                }

                // nếu tạo thành công thì mới tạo câu hỏi
                foreach (var quizQuestionAnswer in model.QuizQuestionAnswers)
                {
                    quizQuestionAnswer.AccountId = accountId;
                    quizQuestionAnswer.QuestionDetailId = questionDetailDb.QuestionDetailID;

                    var questionAnswerDb = CommonDataService.GetQuizQuestionAnswer(quizQuestionAnswer.QuizQuestionAnswerID);

                    if (questionAnswerDb == null)
                    {
                        var newquizQuestionAnswerId = CommonDataService.AddQuizQuestionAnswer(quizQuestionAnswer);

                        if (newquizQuestionAnswerId < 1)
                        {
                            ModelState.AddModelError("ErrorMessage", "Có sự cố khi tạo thông tin câu trả lời cho câu hỏi");
                            return View("Edit", model);
                        }
                    } else
                    {
                        var editquizQuestionAnswerId = CommonDataService.UpdateQuizQuestionAnswer(quizQuestionAnswer);
                        if (!editquizQuestionAnswerId)
                        {
                            ModelState.AddModelError("ErrorMessage", "Có sự cố khi cập nhật thông tin câu trả lời cho câu hỏi");
                            return View("Edit", model);
                        }
                    }
                }

                return View("Edit", model);
            }
        }
        public IActionResult Delete(int id = 0)
        {
            bool result = CommonDataService.DeleteQuestionDetail(id);
            return Json(new { success = result });
        }
    }
}
