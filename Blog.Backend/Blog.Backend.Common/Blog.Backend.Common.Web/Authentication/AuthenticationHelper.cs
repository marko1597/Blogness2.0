using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Common.Web.Authentication
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        public bool SignIn(User user)
        {
            try
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) },
                    DefaultAuthenticationTypes.ApplicationCookie,
                    ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(ClaimTypes.GivenName, string.Format("{0}-{1}", user.FirstName, user.LastName))
                });

                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SignOut(User user)
        {
            try
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
