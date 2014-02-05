using System.Security.Principal;

namespace Blog.Frontend.Web.CustomHelpers.Authentication
{
    public interface ICustomPrincipal : IPrincipal
    {
        int UserId { get; set; }
        string Username { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}