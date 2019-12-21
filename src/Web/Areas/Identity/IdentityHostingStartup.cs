using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EC_Website.Areas.Identity.IdentityHostingStartup))]
namespace EC_Website.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}