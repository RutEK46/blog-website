using Blog.DataLibrary.BusinessLogic;
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
        private IPasswordHasher _passwordHasher;

        public AccountController(
            IAuthorizationService authorizationService,
            IAccountProcessor accountProcessor,
            IPasswordHasher passwordHasher)
        {
            _authorizationService = authorizationService;
            _accountProcessor = accountProcessor;
            _passwordHasher = passwordHasher;
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
                var salt = _passwordHasher.GetRandomSalt();
                var passwordHash = _passwordHasher.GetHash(vm.Password, salt);

                var result = await _accountProcessor.Create(
                    vm.UserName,
                    vm.Email,
                    salt,
                    passwordHash);

                if (result == 1)
                {
                    return RedirectToAction("Login", new LoginViewModel 
                    { 
                        Email = vm.Email, 
                        Password = vm.Password 
                    });
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string redirectUrl="/")
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var user = await _accountProcessor.LoadByEmail(vm.Email);
                var passwordHash = _passwordHasher.GetHash(vm.Password, user.Salt);

                if (user.PasswordHash.Trim() == passwordHash)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var claimIdentity = new ClaimsIdentity(claims, "WEBSITE");

                    var userPrincipal = new ClaimsPrincipal(new[] { claimIdentity });

                    await HttpContext.SignInAsync(userPrincipal);
                }
            }

            return Redirect(redirectUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string redirectUrl="/")
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            return Redirect(redirectUrl);
        }
    }
}
