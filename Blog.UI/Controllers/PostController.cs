using Blog.DataLibrary.BusinessLogic;
using Blog.UI.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private IPostProcessor _postProcessor;

        public PostController(IPostProcessor postProcessor)
        {
            _postProcessor = postProcessor;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/Index/{title}")]
        public async Task<IActionResult> Index(string title)
        {
            var post = await _postProcessor.Load(title);

            if (post is null)
            {
                RedirectToAction("Index", "Home");
            }

            return View(new IndexViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _postProcessor.Create(vm.Title,vm.Body);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [Route("[controller]/Edit/{title}")]
        public async Task<IActionResult> Edit(string title)
        {
            var post = await _postProcessor.Load(title);

            if (post is null)
            {
                RedirectToAction("Index", "Home");
            }

            return View(new EditViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel vm)
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

            return RedirectToAction("Index", new { id = vm.Title });
        }

        [HttpGet]
        [Route("[controller]/Delete/{title}")]
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
