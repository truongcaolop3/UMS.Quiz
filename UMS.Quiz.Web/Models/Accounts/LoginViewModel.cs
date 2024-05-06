using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UMS.Quiz.Web.Models.Accounts
{
    /// <summary>
    /// Mô hình dữ liệu đăng nhập
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Mã giảng viên / mã sinh viên
        /// </summary>
        [Required(ErrorMessage = "{0} bắt buộc phải có")]
        [DisplayName("Tên đăng nhập")]
        public string? UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required(ErrorMessage = "{0} bắt buộc phải có")]
        [DisplayName("Mật khẩu")]
        public string? Password { get; set; }

        /// <summary>
        /// Vai trò
        /// </summary>
        [Required(ErrorMessage = "{0} bắt buộc phải có")]
        [DisplayName("Vai trò")]
        public string? Role { get; set; }
    }
}
