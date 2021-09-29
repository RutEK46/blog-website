using Blog.DataLibrary.BusinessLogic.Processors;
using Blog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogItemProcessor _blogItemProcessor;

        public HomeController(
            ILogger<HomeController> logger,
            IBlogItemProcessor blogItemProcessor)
        {
            _logger = logger;
            _blogItemProcessor = blogItemProcessor;
        }

        public async Task<IActionResult> Index()
        {
            var model = (await _blogItemProcessor.Load()).ToList();
            
            return View(model);
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
