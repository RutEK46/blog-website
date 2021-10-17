using Blog.Domain.Models;
using Blog.UI.AuthorizationRequirements;
using Blog.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            IAuthorizationService authorizationService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _authorizationService = authorizationService;
            _signInManager = signInManager;
            _userManager = userManager;
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
                return View();
            }

            return RedirectToAction("Index", "Home");
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
                var user = new User
                {
                    UserName = vm.UserName,
                    Email = vm.Email
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
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
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {
                    await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                }
            }

            return Redirect(redirectUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string redirectUrl="/")
        {
            await _signInManager.SignOutAsync();

            return Redirect(redirectUrl);
        }
    }
}
