using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VillaBisutti.Delta.WebApp.Startup))]
namespace VillaBisutti.Delta.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
