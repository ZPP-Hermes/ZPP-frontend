using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZPP___frontend.Startup))]
namespace ZPP___frontend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
