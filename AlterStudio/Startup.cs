using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlterStudio.Startup))]
namespace AlterStudio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
