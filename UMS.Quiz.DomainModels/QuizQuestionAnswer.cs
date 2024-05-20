using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Câu hỏi trả lời
    /// </summary>
    public class QuizQuestionAnswer
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int QuizQuestionAnswerID {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? AnswerText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsCorrect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float PercenterValue {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int QuestionDetailId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public QuestionDetail? QuestionDetail { get; set; }

        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }
    }
}
