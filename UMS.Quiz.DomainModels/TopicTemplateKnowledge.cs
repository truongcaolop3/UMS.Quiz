using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DomainModels
{
    public class TopicTemplateKnowledge
    {
        public int TopicTemplateID { get; set; }
        public TopicTemplate TopicTemplate { get; set; } = new TopicTemplate();

        public int KnowledgeID { get; set; }
        public Knowledges Knowledge { get; set; } = new Knowledges();
    }
}
