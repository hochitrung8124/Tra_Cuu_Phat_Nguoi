using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TraCuuPhatNguoi.Models;

namespace TraCuuPhatNguoi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static object resultOk;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TraCuu(string name)
        {

               return Content(await ProcessTraCuuAsync(name));
            //return Content(name);
        }

        private static async Task<string> ProcessTraCuuAsync(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync($"https://www.csgt.vn/tra-cuu-phuong-tien-vi-pham.html?&LoaiXe=2&BienKiemSoat={name}");
                if (res != null && res.IsSuccessStatusCode)
                {
                    var dataHTML = await res.Content.ReadAsStringAsync();
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(dataHTML);
                    HtmlNode node = doc.GetElementbyId("bodyPrint123");
                    return node.InnerHtml;
                }
                return null;
            }

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
