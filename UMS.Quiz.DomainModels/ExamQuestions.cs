using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Bộ đề thi
    /// </summary>
    public class ExamQuestions
    {
        /// <summary>
        /// ID đề thi
        /// </summary>
        [Key]
        public int ExamQuestionID { get; set; }
        /// <summary>
        /// Tên đề thi
        /// </summary>
        public string? ExamQuestionName { get; set; }
        /// <summary>
        /// thời gian thi
        /// </summary>
        public int ExamTime { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool? Status { get; set; }
        /// <summary>
        /// Loại câu hỏi
        /// </summary>
        public int QuestionType { get; set; }
        /// <summary>
        /// Câu hỏi
        /// </summary>
        public string? QuestionText { get; set; }
        /// <summary>
        /// điểm câu hỏi
        /// </summary>
        public int QuestionPoint { get; set; }
        /// <summary>
        /// lấy danh sách chi tiết câu hỏi
        /// </summary>
        //public QuestionDetail? QuestionDetail { get; set; }
        ///// <summary>
        ///// danh sách khối kiến thức
        ///// </summary>
        //public Knowledges? Knowledges { get; set; }
        /// <summary>
        /// Id cấu trúc đề
        /// </summary>
        //public int TopicTemplateID { get; set; }
        //public TopicTemplate? TopicTemplate { get; set; }
        /// <summary>
        /// lấy danh thuộc học phần nào 
        /// </summary>
        public int? TopicTemplateID { get; set; }
        public TopicTemplate? TopicTemplate { get; set; }
        public ICollection<Exam> Exam { get; set; } = new List<Exam>();

        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }
    }
}
