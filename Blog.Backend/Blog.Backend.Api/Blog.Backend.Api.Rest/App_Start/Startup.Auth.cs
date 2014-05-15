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
                LoginPath = new PathString("/Home"),
                ExpireTimeSpan = new TimeSpan(0, 0, 5),
                CookieName = ConfigurationManager.AppSettings.Get("SessionCookieName")
            });
        }
    }
}