using Blog.DataLibrary.BusinessLogic.Processors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLibrary.BusinessLogic
{
    public class AccountManager : IAccountManager
    {
        private IAccountProcessor _accountProcessor;
        private IPasswordHasher _passwordHasher;
        private IHttpContextAccessor _httpContextAccessor;

        public AccountManager(
            IAccountProcessor accountProcessor,
            IPasswordHasher passwordHasher,
            IHttpContextAccessor httpContextAccessor)
        {
            _accountProcessor = accountProcessor;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext HttpContext
            => _httpContextAccessor.HttpContext;

        public async Task<bool> CreateAccount(string userName, string email, string password)
        {
            var salt = _passwordHasher.GetRandomSalt();
            var passwordHash = _passwordHasher.GetHash(password, salt);

            var result = await _accountProcessor.Create(
                userName,
                email,
                salt,
                passwordHash);

            return result == 1;
        }

        public async Task SignInWithPassword(string email, string password)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _accountProcessor.Load(email);
                var passwordHash = _passwordHasher.GetHash(password, user.Salt);

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
        }

        public async Task SignOut()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
            }
        }
    }
}
