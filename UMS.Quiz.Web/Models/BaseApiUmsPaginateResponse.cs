namespace UMS.Quiz.Web.Models
{
    public class BaseApiUmsPaginateResponse<T> where T : class
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }

        public int PageCount { get; set; }

        public IReadOnlyList<T>? Value { get; set; }
    }
}
