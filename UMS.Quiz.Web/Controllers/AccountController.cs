using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using UMS.Quiz.Web.Constants;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using UMS.Quiz.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using System.Security.Claims;
using UMS.Quiz.BusinessLayers;
using UMS.Quiz.DomainModels;
using UMS.Quiz.DataLayers.SQLServer;
using UMS.Quiz.Web.Models.Accounts;
using System.Net.Http.Headers;

namespace UMS.Quiz.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            // Khi đã đăng nhập trước đó thì chuyển về trang chủ
            var accountId = HttpContext.User.Claims.FirstOrDefault();
            if (accountId != null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewData)
        {
            // kiểm tra dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return View(viewData);
            }

            // xử lý đăng nhập
            var account = AccountService.Authorize(viewData.UserName!, viewData.Password!, viewData.Role!);

            if (account == null)
            {
                TempData["ErrorLogin"] = "Tài khoản hoặc mật khẩu không chính xác";
                return View(viewData);
            }

            // Lưu tolen vào trong cookie authentication
            var claims = new List<Claim>
                {
                    new Claim("AccountId", account.AccountId.ToString()),
                    new Claim(nameof(account.ID), account.ID!),
                    new Claim(ClaimTypes.Role, account.Role!),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );


            // Chuyển hướng người dùng đến trang 'Index' của controller 'Home'
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult LoginUms()
        {
            var appId = $"app_id={UmsApiConstant.UMS_APP}";
            var redirectUrl = $"redirect_uri={UmsApiConstant.REDIRECT_URL}";
            var role = $"role={UmsApiConstant.TEACHER_ROLE}";
            var queryParams = $"{appId}&{redirectUrl}&{role}";
            var authorizeUrl = $"{UmsApiConstant.BASE_URL}{UmsApiConstant.GET_AUTH}?{queryParams}";
            return Redirect(authorizeUrl);
        }

        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public AccountController(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UmsApiAuthorizeResponseAsync()
        {

            // Lấy giá trị query string từ yêu cầu
            var umsApiRes = Request.QueryString.Value;
            Console.WriteLine($"===> umsApiRes: {umsApiRes}");

            // Lấy giá trị 'code' từ query string
            var code = HttpUtility.ParseQueryString(umsApiRes!).Get("code");
            Console.WriteLine($"===> code: {code}");

            // Lấy giá trị 'host' từ query string
            var host = HttpUtility.ParseQueryString(umsApiRes!).Get("host");
            Console.WriteLine($"===> host: {host}");

            // Gửi yêu cầu POST đến host để xác thực
            using (HttpClient client = new HttpClient())
            {
                // Tạo dictionary chứa dữ liệu cho yêu cầu POST
                var values = new Dictionary<string, string>
        {
            { nameof(code), code! },  // Thêm 'code' vào dữ liệu
            { "time", UmsApiConstant.UMS_TIME },  // Thêm thời gian (time) vào dữ liệu
            { "signature", UmsApiConstant.UMS_SIGNATURE },  // Thêm chữ ký (signature) vào dữ liệu
        };

                // Tạo nội dung yêu cầu từ dictionary
                var content = new FormUrlEncodedContent(values);

                // Gửi yêu cầu POST đến host và nhận phản hồi
                var response = await client.PostAsync(host, content);

                // Đọc nội dung phản hồi từ server
                var responseString = await response.Content.ReadAsStringAsync();

                // Giải mã phản hồi để lấy token và lưu vào UMS_TOKEN
                UmsApiConstant.UMS_TOKEN = JsonConvert.DeserializeObject<ApiUmsAccountVerifyResponse>(responseString)!.Data!.Token;

                // Lưu tolen vào trong cookie authentication
                var claims = new List<Claim>
                {
                    new Claim(nameof(UmsApiConstant.UMS_TOKEN), UmsApiConstant.UMS_TOKEN!),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );

                Console.WriteLine($"===> UMS token: {UmsApiConstant.UMS_TOKEN}");
            }

            // Chuyển hướng người dùng đến trang 'Index' của controller 'Home'
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Xóa token khỏi Session
            //_httpContextAccessor.HttpContext!.Session.Remove("UMS_TOKEN");
            // UmsApiConstant.UMS_TOKEN = null;

            // Xoá sạch cookie authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Đăng xuất người dùng (tùy vào cách bạn đã cài đặt)
            // Ví dụ: Đặt trạng thái đăng xuất hoặc xóa cookie, v.v.
            // Ở đây mình đặt trạng thái đăng xuất bằng cách đặt đối tượng người dùng hiện tại là null
            HttpContext.User = null!;

            // Chuyển hướng người dùng đến trang đăng nhập hoặc trang chính
            return RedirectToAction("Login", "Account"); // Điều hướng đến trang đăng nhập
        }

        private static async Task<IReadOnlyList<ApiUmsStudyModuleResponse>> UmsStudyModuleApi(string searchValue = "")
        {
            try
            {
                // Khởi tạo một HttpClient để gửi yêu cầu HTTP.
                using (HttpClient client = new())
                {
                    // Tạo URL hoàn chỉnh để gửi yêu cầu.
                    var uri = $"{UmsApiConstant.BASE_URL}{UmsApiConstant.GET_STUDY_MODULE}";
                    // Tạo chuỗi query parameters để thêm vào URL.
                    var queryParams = $"?searchValue={searchValue}";
                    // Thiết lập địa chỉ cơ sở cho HttpClient.
                    client.BaseAddress = new Uri(uri);
                    // Đặt header cho yêu cầu để chỉ rằng client mong đợi nhận dạng JSON.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // Gửi yêu cầu GET đến API và nhận phản hồi.
                    HttpResponseMessage response = await client.GetAsync(queryParams);
                    // Khởi tạo một danh sách chỉ đọc để lưu trữ dữ liệu trả về từ API.
                    IReadOnlyList<ApiUmsStudyModuleResponse> studyModuleValue = null!;
                    // Kiểm tra xem phản hồi có thành công không.
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc nội dung phản hồi dưới dạng chuỗi.
                        var responseString = await response.Content.ReadAsStringAsync();
                        // Giải mã chuỗi JSON thành đối tượng cụ thể.
                        studyModuleValue = JsonConvert.DeserializeObject<BaseApiUmsResponse<BaseApiUmsPaginateResponse<ApiUmsStudyModuleResponse>>>(responseString)!.Data!.Value!;
                    }
                    else
                    {
                        // In ra mã lỗi và lý do lỗi nếu có lỗi.
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }
                    // Trả về kết quả là danh sách dữ liệu hoặc lỗi.
                    return studyModuleValue;
                }
            }
            catch
            {
                // Xử lý ngoại lệ và trả về lỗi 500.
                return null!;
            }
        }

        public static async Task<bool> UpdateAccountWithTermAsync(int accountId, string chooseTermId)
        {
            try
            {
                // Tìm kiếm tài khoản trong cơ sở dữ liệu
                var account = CommonDataService.GetAccount(accountId);
                if (account == null)
                {
                    // Trả về false nếu không tìm thấy tài khoản
                    return false;
                }

                // kiểm tra xem thử chooseTermId có trong csdl hay chưa?
                var termDb = CommonDataService.GetTerm(chooseTermId);
                if (termDb == null)
                {
                    // thêm từ api vào csdl
                    var studyModules = await UmsStudyModuleApi(chooseTermId);

                    foreach ( var studyModule in studyModules)
                    {
                        if (studyModule != null)
                        {
                            var termData = new Terms
                            {
                                TermID = studyModule.MaHocPhan,
                                TermName = studyModule.TenHocPhan,
                                Knowledges = null!,
                            };
                            
                            CommonDataService.AddTerm(termData);
                            //if (isCreated == 0)
                            //{
                            //    return false;
                            //}
                        }
                    }
                } 

                // Cập nhật học phần cho tài khoản
                account.TermId = chooseTermId;
                
                // Lưu thay đổi vào cơ sở dữ liệu
                CommonDataService.UpdateAccount(account);

                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine($"Lỗi khi cập nhật học phần cho tài khoản: {ex.Message}");
                return false;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTerm(string chooseTermId)
        {
            var accountId = HttpContext.User.Claims.FirstOrDefault();
            if (accountId != null)
            {
                Console.WriteLine($"===> ACCOUNT ID IN HOME: {accountId.Value}");
            }

            Console.WriteLine($"===> chooseTermId: {chooseTermId}");

            // Gọi hàm cập nhật học phần cho tài khoản
            var result = await UpdateAccountWithTermAsync(int.Parse(accountId!.Value), chooseTermId);

            if (!result)
            {
                return Json(null);
            }

            //if (result)
            //{
            //    //// Nếu cập nhật thành công, chuyển hướng hoặc trả về thông báo thành công
            //    //return RedirectToAction("Index", "Home"); // Ví dụ chuyển hướng đến trang chính sau khi cập nhật
            //    return Json(result);
            //}
            //else
            //{
            //    // Nếu có lỗi, hiển thị thông báo lỗi
            //    ModelState.AddModelError(string.Empty, "Cập nhật học phần không thành công.");
            //    return View(chooseTermId); // Hiển thị lại view với dữ liệu đã nhập
            //}
            return Json(result);
        }

    }
}
