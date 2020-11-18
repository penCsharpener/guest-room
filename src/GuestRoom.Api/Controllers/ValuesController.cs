using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GuestRoom.Api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get() {
            return Summaries;
        }
    }
}
