using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SeriLog_Seq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Index()
        {
            //_logger.LogInformation("Home index tetiklenmiştir.");
            string text = "Selam, millet!";
            string text2 = "Hello, guys!";
            _logger.LogInformation("{text} değeri loglandı. {text2}",text,text2);
            _logger.LogInformation("{a} değeri loglandı. {b}",text,text2);

            var instance1 = new { Name = "Example", Number = 12345 };
            var instance2 = new { Name = "Example 2", Number = 54321 };
            _logger.LogInformation("{a} değeri loglandı. {b}", instance1, instance2);

            var instance3 = new { Name = "Example", Number = 12345 };
            var instance4 = new { Name = "Example 2", Number = 54321 };
            _logger.LogInformation("{@a} değeri loglandı. {@b}", instance1, instance2);
        }

    }
}

