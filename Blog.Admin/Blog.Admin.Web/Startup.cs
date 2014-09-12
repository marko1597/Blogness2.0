using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog.Admin.Web.Startup))]
namespace Blog.Admin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
