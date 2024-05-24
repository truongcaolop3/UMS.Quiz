using UMS.Quiz.Web.Constants;

namespace UMS.Quiz.Web.Models
{
    /// <summary>
    /// Đầu vào tìm kiếm dữ liệu để nhận dữ liệu dưới dạng phân trang
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public string SearchValue { get; set; } = "";
        public string TermID { get; set; } = "";
        public int accountId { get; set; }
        //public string KnowledgeName { get; set; } = "";
    }

    public class QuestionDetailInput : PaginationSearchInput
    {
        public int QuestionType { get; set; } = QuestionTypeConstant.DEFAULT;
        public int KnowledgeId { get; set; }
        public string? KnowledgeName { get; set; }

    }
    public class TopicTemplateInput : PaginationSearchInput
    {
        public int ExamTime { get; set; } = ExamTimeContant.DEFAULT;
        public int KnowledgeId { get; set; }
        public string? KnowledgeName { get; set; }
    }
}
