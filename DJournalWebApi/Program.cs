using System;
using DJournalWebApi.Data;
using DJournalWebApi.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DJournalWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<Teacher>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                var config = services.GetRequiredService<IConfiguration>();
                Initializer.Initialize(context, userManager,roleManager, config).Wait();
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(opt => { opt.AddJsonFile("appsettings.json"); })
                .UseStartup<Startup>()
                .Build();
    }
}