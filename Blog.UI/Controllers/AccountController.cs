using Blog.DataLibrary.BusinessLogic;
using Blog.DataLibrary.BusinessLogic.Processors;
using Blog.UI.AuthorizationRequirements;
using Blog.UI.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthorizationService _authorizationService;
        private IAccountProcessor _accountProcessor;
        private IAccountManager _accountManager;

        public AccountController(
            IAuthorizationService authorizationService,
            IAccountProcessor accountProcessor,
            IAccountManager accountManager)
        {
            _authorizationService = authorizationService;
            _accountProcessor = accountProcessor;
            _accountManager = accountManager;
        }

        public async Task<AuthorizationResult> AuthorizeAccountAccess(string accountUserName)
        {
            var builder = new AuthorizationPolicyBuilder();
            var accountAccessPolicy = builder
                .RequireAccountAccess(accountUserName)
                .Build();

            return await _authorizationService.AuthorizeAsync(User, accountAccessPolicy);
        }

        [HttpGet]
        [Route("[controller]/Index/{userName}")]
        public async Task<IActionResult> Index(string userName)
        {
            if ((await AuthorizeAccountAccess(userName)).Succeeded)
            {

            }

            return View();
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
                bool isSucceeded = await _accountManager.CreateAccount(
                    vm.UserName,
                    vm.Email,
                    vm.Password);

                if (isSucceeded)
                {
                    await _accountManager.SignInWithPassword(vm.Email, vm.Password);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string redirectUrl="/")
        {
            if (ModelState.IsValid)
            {
                await _accountManager.SignInWithPassword(vm.Email, vm.Password);
            }

            return Redirect(redirectUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string redirectUrl="/")
        {
            await _accountManager.SignOut();

            return Redirect(redirectUrl);
        }
    }
}
