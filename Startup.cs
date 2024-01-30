using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPV_Mark3.Startup))]
namespace CPV_Mark3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
