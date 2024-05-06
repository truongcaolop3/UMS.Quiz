using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Tài khoản của ứng dụng
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Mã tự tăng của account.
        /// </summary>
        [Key]
        public int AccountId { get; set; }

        /// <summary>
        /// Mã giảng viên / sinh viên.
        /// </summary>
        [Required]
        public string? ID { get; set; }

        /// <summary>
        /// Mật khẩu.
        /// </summary>
        [Required]
        public string? Password { get; set; }

        /// <summary>
        /// Họ và tên.
        /// </summary>
        [Required]
        public string? FullName { get; set; }

        /// <summary>
        /// Ngày sinh.
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// Địa chỉ email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Loại tài khoản.
        /// </summary>
        [Required]
        public string? Role { get; set; }

        /// <summary>
        /// Mã học phần tác nghiệp của giảng viên.
        /// </summary>
        public string? TermId { get; set; }
    }
}
