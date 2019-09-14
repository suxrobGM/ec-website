using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EC_WebSite.Areas.Identity.IdentityHostingStartup))]
namespace EC_WebSite.Areas.Identity
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