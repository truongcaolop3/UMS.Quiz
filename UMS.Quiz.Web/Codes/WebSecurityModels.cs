using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;

namespace UMS.Quiz.Web.Codes
{
    public class WebSecurityModels
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public string? ClientIP { get; set; }
        public string? SessionId { get; set; }
        public string? AdditionalData { get; set; }
        public List<string>? Roles { get; set; }
    }
    
    /// <summary>
    /// Thông tin về nhóm/quyền
    /// </summary>
    public class WebUserRole
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Tên/ký hiệu nhóm/quyền</param>
        /// <param name="description">Mô tả</param>
        public WebUserRole(string name, string description)
        {
            Name = name;
            Description = description;
        }
        /// <summary>
        /// Tên/Ký hiệu quyền
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
    /// <summary>
    /// Danh sách các nhóm quyền sử dụng trong ứng dụng
    /// </summary>
    public class WebUserRoles
    {
        /// <summary>
        /// Lấy danh sách thông tin các Role dựa vào các hằng được định nghĩa trong lớp này
        /// </summary>
        public static List<WebUserRole> ListOfRoles
        {
            get
            {
                List<WebUserRole> listOfRoles = new List<WebUserRole>();

                Type type = typeof(WebUserRoles);
                var listFields = type.GetFields(BindingFlags.Public
                                                | BindingFlags.Static
                                                | BindingFlags.FlattenHierarchy)
                               .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string));

                foreach (var role in listFields)
                {
                    string? roleName = role.GetRawConstantValue() as string;
                    if (roleName != null)
                    {
                        DisplayAttribute? attribute = role.GetCustomAttribute<DisplayAttribute>();
                        if (attribute != null)
                            listOfRoles.Add(new WebUserRole(roleName, attribute.Name ?? roleName));
                        else
                            listOfRoles.Add(new WebUserRole(roleName, roleName));
                    }
                }
                return listOfRoles;
            }
        }

        //TODO: Định nghĩa các role được sử dụng trong hệ thống tại đây

        [Display(Name = "Quản trị hệ thống")]
        public const string Lecture = "admin";

        //[Display(Name = "Nhân viên")]
        //public const string Knowledges = "knowledges";

        //[Display(Name = "Khách hàng")]
        //public const string Quizzes = "quizzes";
    }
}
