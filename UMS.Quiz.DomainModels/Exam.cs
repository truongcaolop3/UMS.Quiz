using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Danh sách các cuộc thi
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// Id danh sách của cuộc thi
        /// </summary>
        [Key]
        public int ExamID { get; set; }
        /// <summary>
        /// Tên cuộc thi
        /// </summary>
        public string? ExamName { get; set; }
        /// <summary>
        /// Thời gian bắt đầu có thể làm bài 
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// Thời gian kết thúc không thể làm bài nữa
        /// Quá giờ hệ thống sẽ tự động không cho làm và thu bài
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// ID bộ đề thi
        /// </summary>
        public int? ExamQuestionID { get; set; }
        public ExamQuestions? ExamQuestions { get; set; }


        public ICollection<ExamDetailCandidates> examDetailCandidates { get; set; } = new List<ExamDetailCandidates>();
        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }
    }
}
