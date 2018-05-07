using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecKevinKeizoKASP.Startup))]
namespace RecKevinKeizoKASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
