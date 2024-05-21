using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers
{
    public interface IQuizQuestionAnswerDAL : ICommonDAL<QuizQuestionAnswer>
    {
        IList<QuizQuestionAnswer> GetQuizQuestionAnswerByQuestionDetailId(int questionDetailId);
    }
}
