using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// danh sách thí sinh trong một cuộc thi
    /// </summary>
    public class ExamDetailCandidates
    {
        [Key]
        public int ExamDetailCandidatesID { get; set; }
        /// <summary>
        /// Mã sinh viên not null
        /// </summary>
        public string? StudentID { get; set; }
        /// <summary>
        /// Tên sinh viên not null
        /// </summary>
        public string? StudentName { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// địa chỉ Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Tài khoản của từng sinh viên
        /// Tài khoản này sẽ tự sinh khi thêm sinh viên vào danh sách thành công
        /// </summary>
        public string? AccountStudent {  get; set; }
        /// <summary>
        /// Mật khẩu của từng sinh viên
        /// Mật khẩu này sẽ tự sinh khi thêm sinh viên vào danh sách thành công
        /// </summary>
        public string? PassworkStudent { get; set; }
        
       
        /// <summary>
        /// Id danh sách của cuộc thi
        /// </summary>
        public int? ExamID { get; set; }
        /// <summary>
        /// Thuộc cuộc thi nào 
        /// lấy thông tin của cuộc thi đó 
        /// </summary>
        public Exam? Exam { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<ExamDetailAnswer> examDetailAnswers { get; set; } = new List<ExamDetailAnswer>();

        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }
    }
}
