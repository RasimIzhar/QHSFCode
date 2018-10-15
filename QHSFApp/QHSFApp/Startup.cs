using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QHSFApp.Startup))]
namespace QHSFApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
