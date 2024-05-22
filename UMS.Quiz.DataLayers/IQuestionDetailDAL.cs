using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers
{
    public interface IQuestionDetailDAL : ICommonDAL<QuestionDetail>
    {
        IList<QuestionDetail> List(int page = 1, int pageSize = 0, string searchValue = "", int questionType = 0, int knowledgeId = 0, int AccountId = 0);

        int Count(string searchValue = "", int questionType = 0, int knowledgeId = 0, int AccountId = 0);

        
    }
}
