using Microsoft.AspNetCore.Mvc;
using prjSite.Models;
using System.Diagnostics;

namespace prjSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult first()
        {
            return View();
        }
        public IActionResult GetDemo()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Address()
        {
            return View();
        }
        public IActionResult Promise()
        {
            return View();
        }
        public IActionResult Fetch()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult jQuery()
        {
            return View();
        }

        //使用partial view 就不會有layout的版面，可以在其他view中呼叫使用@HTML.Partial("Action名稱")

        public IActionResult partial1()
        {
            return PartialView();
        }
        //這邊的重點在於，如果是在其他view中呼叫使用@HTML.Partial("Action名稱")，這樣只會有 partialview中的畫面，不會通過action，因此資料是不會傳過去的
        //而如果使用ajax的話就可以解決這個問題，所以如果要使用partialview包含資料的話，那就要使用ajax，不然就要在用viewcomponet比較麻煩
        public IActionResult partial2()
        {
            ViewBag.message = "來自Action的內容";
            return PartialView();
        }

        //basic 套件
        public IActionResult dataTables()
        {
            return View();
        }

        //套API
        public IActionResult dataTablesAPI()
        {
            return View();
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