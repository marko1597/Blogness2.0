using System;
using System.Security.Principal;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Frontend.Web.Authentication
{
    public class BlogIdentity : IIdentity
    {
        public String Name { get; private set; }
        public Boolean IsAuthenticated { get; private set; }
        public User User { get; private set; }
        public Session Session { get; private set; }

        public BlogIdentity(LoggedUser loggedUser)
        {
            User = loggedUser.User;
            Session = loggedUser.Session;
            Name = loggedUser.User.UserName;
            IsAuthenticated = loggedUser.Session != null && string.IsNullOrEmpty(loggedUser.Session.Token) && loggedUser.Session.TimeValidity <= DateTime.Now;
        }

        public string AuthenticationType
        {
            get { return string.Empty; }
        }
    }
}