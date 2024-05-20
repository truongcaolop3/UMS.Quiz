using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Cấu trúc đề
    /// </summary>
    public class TopicTemplate
    {
        /// <summary>
        /// Id cấu trúc đề
        /// </summary>
        [Key]
        public int TopicTemplateID { get; set; }
        /// <summary>
        /// Tên cấu trúc đề
        /// </summary>
        public string? TopicTemplateName { get; set; } 
        /// <summary>
        /// thời gian thi
        /// </summary>
        public int ExamTime { get; set; }
        /// <summary>
        /// số điểm cần lấy của một khối kiến thức đưa vào cấu trúc bộ đề 
        /// </summary>
        public int PointGet { get; set; }
        /// <summary>
        /// số câu hỏi cần lấy của một khối kiến thức đưa vào cấu trúc bộ đề 
        /// </summary>
        public int QuantityGet { get; set; }
        
        public ICollection<QuestionDetail> questionDetails { get; set; } = new List<QuestionDetail>();

        /// <summary>
        /// lấy danh sách khối kiến thức
        /// </summary>
        public ICollection<Knowledges> Knowledges { get; set; } = new HashSet<Knowledges>();
        //public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
        public ICollection<ExamQuestions> ExamQuestions { get; set; } = new List<ExamQuestions>();

        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }
    }
}
