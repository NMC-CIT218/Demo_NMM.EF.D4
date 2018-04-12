using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demo_NMM.EF.Startup))]
namespace Demo_NMM.EF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
