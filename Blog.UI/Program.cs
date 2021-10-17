using Blog.Database;
using Blog.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var build = CreateHostBuilder(args).Build();

            CreateUsersAndRoles(build.Services.CreateScope())
                .GetAwaiter()
                .GetResult();

            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task CreateUsersAndRoles(IServiceScope scope)
        {
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            ctx.Database.EnsureCreated();

            var roles = new List<Role>
            {
                new Role("Admin"),
                new Role("Moderator")
            };

            foreach (var role in roles)
            {
                if (!await ctx.Roles.AnyAsync(x => x.Name == role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!await ctx.Users.AnyAsync(u => u.UserName == "Admin"))
            {
                var adminUser = new User
                {
                    UserName = "Admin",
                    Email = config["Admin:Email"]
                };

                var result = await userManager.CreateAsync(adminUser, config["Admin:Password"]);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
