using Humanizer.Bytes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using prjSite.Models;
using System.Drawing;
using System.Xml.Schema;

namespace prjSite.Controllers
{
    public class ApiController : Controller
    {
        //需要使用一個 全域變數接收整個DemoContext 
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;

        //為了達到這個目的，使用建構子，在建立的時候就將其指定給_context騡域變數，
        //第二個參數是為了要上傳照片檔案，所以用了 IWebHostEnvironment這個類別，同樣也要指定到一個 全域變數中
        public ApiController(DemoContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }
        public IActionResult Index()
        {
            //System.Threading.Thread.Sleep(5000);
            return Content("Hello Fetch!!");
        }

        public IActionResult getDemo( UserViewModel user)//public IActionResult getDemo(string name, int age = 30)
        {
            if (String.IsNullOrEmpty(user.name))
            {
                user.name = "Guest";
            }
            return Content($"Hello {user.name}, you are {user.age} years old, thank you for your registeration.");
        }
        public IActionResult Register(Members member, IFormFile file) //除了member要做為參數，
                                                                      //另外也要用IFormFile這個類別，因為在瀏覽器上船片，檔案中包含了很多資料，所以用這個類別先接收所有的資料，然後在點到你要的資料
        {
            //找到檔案路徑，並且放到資料夾中
            string filepath = Path.Combine(_host.WebRootPath, "images", file.FileName);
            using(var filestream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyTo(filestream);
            }

            //把照片傳到資料庫
            byte[]? imgByte = null;
            using(var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileName = file.FileName;
            member.FileData = imgByte;
            //return Content($"檔案名稱{file.FileName} 成功上傳" );
            _context.Members.Add(member);
            _context.SaveChanges();
            return Content("新增成功");
        }
        public IActionResult GetImageByte(int id =1)
        {
            Members? memberid = _context.Members.Find(id);
            byte[] image = memberid.FileData;
            return File(image,"image/jpeg");
        }

        public IActionResult nameCheck(Members member)
        {
            foreach(var i in _context.Members)
            {
                if (i.Name == member.Name)
                    return Content("此名字已註冊過");
            }
            return Content("");
        }

        //
        public IActionResult cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
               return Json(cities);
        }
        public IActionResult District(string city)
        {
            var districts = _context.Address.Where(a=>a.City ==city).Select(c=>c.SiteId).Distinct();
            return Json(districts);

        }
        public IActionResult Roads(string siteId)
        {
            var roads = _context.Address.Where(a => a.SiteId == siteId).Select(r => r.Road).Distinct();
            return Json(roads);
        }
    }
}
