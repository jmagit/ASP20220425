using DemosMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemosMVC.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private PositionOptions positionOptions = new PositionOptions();
        private string userName;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration) {
            positionOptions = new PositionOptions();
            configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            userName = configuration.GetSection("Position")["Name"];
            _logger = logger;
        }

        public IActionResult Index() {
            ViewBag.nombre = userName;
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
