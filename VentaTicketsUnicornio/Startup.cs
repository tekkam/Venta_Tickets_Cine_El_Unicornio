using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VentaTicketsUnicornio.Startup))]
namespace VentaTicketsUnicornio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
