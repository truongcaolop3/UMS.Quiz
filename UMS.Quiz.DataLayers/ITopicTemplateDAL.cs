using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers
{
    internal interface ITopicTemplateDAL : ICommonDAL<TopicTemplate>
    {
        IList<TopicTemplate> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "", int knowledgeId = 0, int AccountId = 0);

        int Count(string searchValue = "", string termId = "", int knowledgeId = 0, int AccountId = 0);
    }

}
