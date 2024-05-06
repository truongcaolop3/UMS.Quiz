namespace UMS.Quiz.Web.Constants
{
    public static class UmsApiConstant
    {
        public const string UMS_APP = "TestApp";

        public const string UMS_SECURITY = "1234567890";

        public static string UMS_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");

        public static string UMS_SIGNATURE = $"{UMS_APP}{UMS_SECURITY}{UMS_TIME}";

        public static string? UMS_TOKEN { get; set; }

        public const string BASE_URL = "https://ums-dev.husc.edu.vn/apigateway";

        public const string REDIRECT_URL = "https://localhost:7137/Account/UmsApiAuthorizeResponse";

        public const string TEACHER_ROLE = "teacher";

        public const string GET_AUTH = "/auth/account/authorize";

        public const string GET_ACCOUNT_PROFILE = "/account/v1/profile";

        public const string GET_STUDY_MODULE = "/common/v1/module/list";

    }
}
