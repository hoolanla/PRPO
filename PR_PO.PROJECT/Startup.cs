using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PR_PO.PROJECT.Startup))]
namespace PR_PO.PROJECT
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
