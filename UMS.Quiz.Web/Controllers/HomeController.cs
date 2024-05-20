using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UMS.Quiz.BusinessLayers;
using UMS.Quiz.Web.Models;

namespace UMS.Quiz.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var token = HttpContext.User.Claims.FirstOrDefault();
            if (token != null)
            {
                Console.WriteLine($"===> UMS TOKEN IN HOME: {token.Value}");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetCurrentTerm()
        {
            var accountId = HttpContext.User.Claims.FirstOrDefault();
            var accountDb = CommonDataService.GetAccount(int.Parse(accountId!.Value));
            if (accountDb == null)
            {
                return Json(null);
            }

            var termDb = CommonDataService.GetTerm(accountDb.TermId!);

            if (termDb == null)
            {
                return Json(null);
            }

            return Json(termDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
