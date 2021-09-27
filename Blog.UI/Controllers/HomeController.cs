using Blog.DataLibrary.BusinessLogic;
using Blog.UI.Models;
using Blog.UI.Models.Posts;
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
        private readonly IPostProcessor _postProcessor;

        public HomeController(
            ILogger<HomeController> logger,
            IPostProcessor postProcessor
            )
        {
            _logger = logger;
            _postProcessor = postProcessor;
        }

        public async Task<IActionResult> Index()
        {
            var model = (await _postProcessor.Load())
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Body = x.Body
                }).ToList();
            
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
