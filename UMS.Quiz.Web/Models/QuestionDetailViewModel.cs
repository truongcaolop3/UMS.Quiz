using UMS.Quiz.DomainModels;

namespace UMS.Quiz.Web.Models
{
    public class QuestionDetailViewModel
    {
        public List<QuestionDetail> QuestionDetails { get; set; } = new List<QuestionDetail>();
        public List<QuizQuestionAnswer> QuestionAnswers { get; set; } = new List<QuizQuestionAnswer>();
        public List<Knowledges> Knowledges { get; set; } = new List<Knowledges>();
    }
}
