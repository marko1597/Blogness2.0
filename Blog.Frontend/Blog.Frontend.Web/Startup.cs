using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog.Frontend.Web.App_Start.Startup))]
namespace Blog.Frontend.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
