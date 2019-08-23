using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiplexData;

[assembly: HostingStartup(typeof(Multiplex.Areas.Identity.IdentityHostingStartup))]
namespace Multiplex.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MultiplexDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MultiplexDbContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                   // .AddEntityFrameworkStores<MultiplexDbContext>();


                services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddDefaultUI()
                        .AddDefaultTokenProviders()
                        .AddEntityFrameworkStores<MultiplexDbContext>();
            });
        }
    }
}