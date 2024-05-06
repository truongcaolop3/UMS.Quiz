namespace UMS.Quiz.Web.Models
{
    public class BaseApiUmsResponse<T> where T : class
    {
        public T? Data { get; set; }

        public int Code { get; set; }

        public string? Msg { get; set; }
    }
}
