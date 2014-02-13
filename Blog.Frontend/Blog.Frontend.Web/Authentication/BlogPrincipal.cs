using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using System.Security.Principal;

namespace Blog.Frontend.Web.Authentication
{
    public class BlogPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public BlogPrincipal(LoggedUser loggedUser)
        {
            Identity = new BlogIdentity(loggedUser);
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}