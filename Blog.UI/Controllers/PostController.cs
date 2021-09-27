using Blog.DataLibrary.BusinessLogic;
using Blog.UI.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    public class PostController : Controller
    {
        private IPostProcessor _postProcessor;

        public PostController(IPostProcessor postProcessor)
        {
            _postProcessor = postProcessor;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _postProcessor.Create(vm.Title,vm.Body);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [Route("[controller]/View/{title}")]
        public async Task<IActionResult> GetView(string title)
        {
            var post = await _postProcessor.Load(title);
            
            if (post is null)
            {
                RedirectToAction("Index", "Home");
            }

            return View("View", new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string title)
        {
            var post = await _postProcessor.Load(title);

            if (post is null)
            {
                RedirectToAction("Index", "Home");
            }

            return View(new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = await _postProcessor.Load(vm.Id);

            if (post is null)
            {
                RedirectToAction("Index", "Home");
            }

            await _postProcessor.Update(
                vm.Id,
                vm.Title,
                vm.Body
                );

            return RedirectToAction("View", new { id = vm.Title });
        }

        public async Task<IActionResult> Delete(string title)
        {
            var post = await _postProcessor.Load(title);

            if (post is null)
            {
                RedirectToAction("Index", "Home");
            }

            await _postProcessor.Delete(post.Id);

            return RedirectToAction("Index", "Home");
        }
    }
}
