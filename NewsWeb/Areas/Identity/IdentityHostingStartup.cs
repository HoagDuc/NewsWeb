using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsWeb.Areas.Identity.Data;
using NewsWeb.Data;

[assembly: HostingStartup(typeof(NewsWeb.Areas.Identity.IdentityHostingStartup))]
namespace NewsWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<NewsWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("NewsWebContextConnection")));

                services.AddDefaultIdentity<NewsWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<NewsWebContext>();
            });
        }
    }
}