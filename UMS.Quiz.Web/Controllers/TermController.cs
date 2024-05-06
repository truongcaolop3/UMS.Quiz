using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using UMS.Quiz.Web.Constants;
using UMS.Quiz.Web.Models;

namespace UMS.Quiz.Web.Controllers
{
    [Authorize]
    public class TermController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string searchValue = "")
        {
            try
            {
                // Khởi tạo một HttpClient để gửi yêu cầu HTTP.
                using (HttpClient client = new())
                {
                    // Tạo URL hoàn chỉnh để gửi yêu cầu.
                    var uri = $"{UmsApiConstant.BASE_URL}{UmsApiConstant.GET_STUDY_MODULE}";
                    // Tạo chuỗi query parameters để thêm vào URL.
                    var queryParams = $"?searchValue={searchValue}&pageSize=0";
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
                    return Ok(studyModuleValue);
                }
            }
            catch
            {
                // Xử lý ngoại lệ và trả về lỗi 500.
                return BadRequest(new
                {
                    statusCode = 500,
                    errorMessage = "Server lỗi rồi",
                });
            }
        }

    }
}
