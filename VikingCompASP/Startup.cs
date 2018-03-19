using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VikingCompASP.Startup))]
namespace VikingCompASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
