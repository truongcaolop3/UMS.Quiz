﻿using Microsoft.AspNetCore.Mvc;
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

namespace UMS.Quiz.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
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
        public async Task<IActionResult> LogoutAsync()
        {
            // Xóa token khỏi Session
            //_httpContextAccessor.HttpContext!.Session.Remove("UMS_TOKEN");
            UmsApiConstant.UMS_TOKEN = null;

            // Xoá sạch cookie authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Đăng xuất người dùng (tùy vào cách bạn đã cài đặt)
            // Ví dụ: Đặt trạng thái đăng xuất hoặc xóa cookie, v.v.
            // Ở đây mình đặt trạng thái đăng xuất bằng cách đặt đối tượng người dùng hiện tại là null
            HttpContext.User = null!;

            // Chuyển hướng người dùng đến trang đăng nhập hoặc trang chính
            return RedirectToAction("Login", "Account"); // Điều hướng đến trang đăng nhập
        }
    }
}
