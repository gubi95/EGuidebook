using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EGuidebook.Startup))]
namespace EGuidebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
