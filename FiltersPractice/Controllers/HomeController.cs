using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using FiltersPractice.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace FiltersPractice.Controllers
{
    //[Message("This is controller scoped filter", Order = 1)]
    [HttpOnly]
    [SimpleResourceFilterAsync]
    public class HomeController : Controller
    {
        //[Message("This is index method attribute", Order = 2)]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }

        [Profile]
        //[Message("This is profile method attribute", Order = 2)]
        public IActionResult Profile()
        {
            return View("Message", "This is the profile model");
        }

        [RangeException]
        //[Message("This is range exception method attribute", Order = 2)]
        public IActionResult RangeException(int id)
        {

            if (id < 18)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Id can't be lower than 18");
            }

            return View("Message", "This is the range exception model");
        }

        [ViewDetails]
        //[Message("This is view details method attribute", Order = 2)]
        public IActionResult ViewDetails()
        {
            return View("Message", "This is the view details model");
        }

        public FileResult ResourceTester([FromServices] IWebHostEnvironment environment) => File(new FileStream(Path.Combine(environment.WebRootPath, "Avatar.jpg"), FileMode.Open), "image/jpeg");
    }
}
