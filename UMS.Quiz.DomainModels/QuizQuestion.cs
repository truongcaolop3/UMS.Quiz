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
        public int KnowledgeId { get; set; }
        public Knowledges? Knowledge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<QuestionDetail> questionDetails { get; set; } = new List<QuestionDetail>();

        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }

    }
}
