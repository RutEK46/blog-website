using Blog.Database;
using Blog.UI.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain.Models;
using System.Security.Claims;

namespace Blog.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly AppDbContext _ctx;

        public PostController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/Index/{title}")]
        public async Task<IActionResult> Index(string title)
        {
            var post = await _ctx.Posts
                .Where(x => x.Title == title)
                .FirstAsync();

            if (post is null)
            {
                return new StatusCodeResult(404);
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
                _ctx.Posts.Add(new Post
                {
                    Title = vm.Title,
                    Body = vm.Body,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                });

                await _ctx.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [Route("[controller]/Edit/{title}")]
        public async Task<IActionResult> Edit(string title)
        {
            var post = await _ctx.Posts
                .Where(x => x.Title == title)
                .FirstOrDefaultAsync();

            if (post is null)
            {
                return new StatusCodeResult(404);
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
            var post = await _ctx.Posts
                .Where(x => x.Id == vm.Id)
                .FirstOrDefaultAsync();

            if (post is null)
            {
                return new StatusCodeResult(404);
            }

            post.Title = vm.Title;
            post.Body = vm.Body;

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index", new { id = vm.Title });
        }

        [HttpGet]
        [Route("[controller]/Delete/{title}")]
        public async Task<IActionResult> Delete(string title)
        {
            var post = await _ctx.Posts
                .Where(x => x.Title == title)
                .FirstOrDefaultAsync();

            if (post is null)
            {
                return new StatusCodeResult(404);
            }

            _ctx.Posts.Remove(post);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
