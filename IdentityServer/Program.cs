using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var userManage = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var user = new IdentityUser("bob");
                userManage.CreateAsync(user, "password").GetAwaiter().GetResult();

                userManage.AddClaimAsync(user, new Claim("rc.garndma", "big.cookie")).GetAwaiter().GetResult();

                userManage.AddClaimAsync(user, new Claim("rc.api.garndma", "big.api.cookie")).GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}