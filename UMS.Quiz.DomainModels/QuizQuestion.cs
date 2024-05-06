using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// danh sách thư viện câu hỏi trắc nghiệm
    /// </summary>
    public class QuizQuestion
    {
        /// <summary>
        /// ID thư viện câu hỏi
        /// </summary>
        [Key]
        public int QuizQuestionId { get; set; }
        /// <summary>
        /// số câu hỏi
        /// </summary>
        public int QuizNumber { get; set; }
        /// <summary>
        /// danh sách khối kiến thức of học phần
        /// </summary>
        public Knowledges? Knowledge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public QuestionDetail? QuestionDetail { get; set; }

    }
}
