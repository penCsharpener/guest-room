using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GuestRoom.Api.Controllers
{
    public class Fallback : Controller
    {
        private readonly IWebHostEnvironment _env;

        public Fallback(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(_env.WebRootPath, "index.html"), "text/HTML");
        }
    }
}
