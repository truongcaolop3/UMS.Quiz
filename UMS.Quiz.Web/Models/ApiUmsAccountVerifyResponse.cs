namespace UMS.Quiz.Web.Models
{
    public class ApiUmsAccountVerifyResponse
    {
        public class DataResponse
        {
            public string? AppId { get; set; }

            public string? Token { get; set; }
        }

        public DataResponse? Data { get; set; }

        public int Code { get; set; }

        public string? Msg { get; set; }
    }
}
