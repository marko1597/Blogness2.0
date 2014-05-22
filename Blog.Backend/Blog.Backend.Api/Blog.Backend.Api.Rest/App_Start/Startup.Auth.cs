using System;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Blog.Backend.Api.Rest
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Unauthorized"),
                ExpireTimeSpan = new TimeSpan(0, 12, 0, 0),
                CookieName = ConfigurationManager.AppSettings.Get("SessionCookieName")
            });
        }
    }
}