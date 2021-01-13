using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Hackathon.Areas.Identity.IdentityHostingStartup))]
namespace Hackathon.Areas.Identity
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