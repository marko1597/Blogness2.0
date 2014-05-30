using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Security.Claims;
using System.Web;
using Blog.Common.Utils.Extensions;
using Blog.Common.Web.Extensions.Elmah;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Blog.Common.Contracts;

namespace Blog.Common.Web.Authentication
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        [Import]
        public IErrorSignaler ErrorSignaler { get; set; }

        private IAuthenticationManager _authenticationManager;
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authenticationManager ?? (_authenticationManager = HttpContext.Current.GetOwinContext().Authentication);
            }
            set { _authenticationManager = value; }
        }

        public bool SignIn(User user)
        {
            try
            {
                if (user == null || user.Error != null)
                {
                    return false;
                }

                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) },
                    DefaultAuthenticationTypes.ApplicationCookie,
                    ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(ClaimTypes.GivenName, string.Format("{0}-{1}", user.FirstName, user.LastName))
                });

                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

                return true;
            }
            catch (Exception ex)
            {
                ErrorSignaler.SignalFromCurrentContext(ex);
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool SignOut(User user)
        {
            try
            {
                if (user == null || user.Error != null)
                {
                    return false;
                }

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return true;
            }
            catch (Exception ex)
            {
                ErrorSignaler.SignalFromCurrentContext(ex);
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
