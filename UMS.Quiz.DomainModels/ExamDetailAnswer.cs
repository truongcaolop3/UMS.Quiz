using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Danh sách câu trả lời của từng sinh viên 
    /// 
    /// </summary>
    public class ExamDetailAnswer
    {
        [Key]
        public int ExamDetailAnswerID { get; set; }
        /// <summary>
        /// Điểm tính theo trọng số
        /// </summary>
        public float Point {  get; set; }
        /// <summary>
        /// Tổng Số điểm Tính theo trọng số
        /// </summary>
        public float AllPoint { get; set; }

        public int ExamDetailCandidatesID { get; set; }
        public ExamDetailCandidates? ExamDetailCandidates { get; set; }
        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }
    }
}
 