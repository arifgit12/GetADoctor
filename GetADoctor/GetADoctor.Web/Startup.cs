using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GetADoctor.Web.Startup))]
namespace GetADoctor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
