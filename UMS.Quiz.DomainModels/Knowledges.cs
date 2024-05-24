using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    /// <summary>
    /// Danh sách khối kiến thức
    /// </summary>
    public class Knowledges
    {
        /// <summary>
        /// Mã khối kiến thức
        /// </summary>
        [Key]
        public int KnowledgeId { get; set; }
        /// <summary>
        /// Tên khối kiến thức
        /// </summary>
        public string? KnowledgeName { get; set;}
        /// <summary>
        /// lấy danh thuộc học phần nào 
        /// </summary>
        public string? TermID { get; set; }
        public Terms? Terms { get; set; }
        /// <summary>
        /// Id cấu trúc đề
        /// </summary>
        //public int? TopicTemplateID { get; set; }
        //public TopicTemplate? TopicTemplate { get; set; }
        public ICollection<TopicTemplateKnowledge> TopicTemplateKnowledges { get; set; } = new List<TopicTemplateKnowledge>();

        //public QuizQuestion? QuizQuestion { get; set; }

        /// <summary>
        /// thuộc người dùng nào 
        /// </summary>
        public int AccountId { get; set; }
        public ICollection<QuestionDetail> questionDetails { get; set; } = new List<QuestionDetail>();
    }
}
