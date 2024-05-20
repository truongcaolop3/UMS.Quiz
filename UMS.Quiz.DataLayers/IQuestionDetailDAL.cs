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

        ///// <summary>
        ///// Lấy một bản ghi/dòng dữ liệu dựa trên mã (id)
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //QuestionDetail? Get(int id);

        ///// <summary>
        ///// Lấy một bản ghi/dòng dữ liệu dựa trên mã (id)
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //QuestionDetail? Get(string id);

        ///// <summary>
        ///// Bổ sung dữ liệu vào trong CSDL. Hàm trả về ID của dữ liệu được bổ sung.
        ///// (Trả về giá trị nhỏ hơn hoặc bằng 0 nếu lỗi)
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //int Add(QuestionDetail data);
        ///// <summary>
        ///// Cập nhật dữ liệu
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //bool Update(QuestionDetail data);
        ///// <summary>
        ///// Xóa một bản ghi dữ liệu dựa vào id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //bool Delete(int id);

        ///// <summary>
        ///// Kiểm tra xem một bản ghi dữ liệu có mã id hiện đang có được sử dụng bởi các bảng khác hay không?
        ///// (có dữ liệu liên quan hay không?)
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //bool IsUsed(int id);
    }
}
