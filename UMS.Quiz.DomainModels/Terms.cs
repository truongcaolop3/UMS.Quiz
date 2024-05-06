using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Học phần
    /// </summary>
    public class Terms
    {
        /// <summary>
        /// Mã học phần
        /// </summary>
        [Key]
        public string? TermID {  get; set; }
        /// <summary>
        /// Tên học phần
        /// </summary>
        public string? TermName { get; set; }

        /// <summary>
        /// Danh sách khối kiến thức
        /// </summary>
        public ICollection<Knowledges> Knowledges { get; set; } = new List<Knowledges>(); // Mối quan hệ với Knowledges

    }
}
