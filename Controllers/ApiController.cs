using Microsoft.AspNetCore.Mvc;

namespace prjSite.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello Ajax!!");
        }
    }
}
