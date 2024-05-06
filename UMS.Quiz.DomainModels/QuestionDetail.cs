using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Danh sách chi tiết câu hỏi
    /// câu hỏi, loại câu hỏi, điểm ...
    /// </summary>
    public class QuestionDetail
    {
        /// <summary>
        /// id câu hỏi
        /// </summary>
        [Key]
        public int QuestionDetailID { get; set; }
        /// <summary>
        /// Loại câu hỏi
        /// </summary>
        public string? QuestionType { get; set; }
        /// <summary>
        /// Câu hỏi
        /// </summary>
        public string? QuestionText { get; set; }
        /// <summary>
        /// điểm câu hỏi
        /// </summary>
        public int QuestionPoint {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int QuizQuestionId { get; set; }
        /// <summary>
        /// thuộc thư viện câu hỏi nào 
        /// </summary>
        public QuizQuestion? QuizQuestion { get; set; }
        public ICollection<QuizQuestionAnswer> QuizQuestionAnswers { get; set; } = new List<QuizQuestionAnswer>();
        /// <summary>
        /// Id cấu trúc đề
        /// </summary>
        public int TopicTemplateID { get; set; }
        public TopicTemplate? TopicTemplate { get; set; }


    }
}
