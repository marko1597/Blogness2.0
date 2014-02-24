using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Frontend.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Name { get; private set; }
        public Boolean IsAuthenticated { get; private set; }
        public User User { get; private set; }
        public Session Session { get; private set; }

        public ApplicationUser(LoggedUser loggedUser)
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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public override IDbSet<ApplicationUser> Users { get; set; }
    }

    public class UserStore : IUserPasswordStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>
    {
        readonly UserStore<IdentityUser> _userStore = new UserStore<IdentityUser>(new ApplicationDbContext());

        public UserStore()
        {
        }

        public Task CreateAsync(ApplicationUser user)
        {
            var context = _userStore.Context as ApplicationDbContext;
            context.Users.Add(user);
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();
        }
        public Task DeleteAsync(ApplicationUser user)
        {
            var context = _userStore.Context as ApplicationDbContext;
            context.Users.Remove(user);
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var context = _userStore.Context as ApplicationDbContext;
            return context.Users.Where(u => u.Id.ToLower() == userId.ToLower()).FirstOrDefaultAsync();
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var context = _userStore.Context as ApplicationDbContext;
            return context.Users.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }
        
        public Task UpdateAsync(ApplicationUser user)
        {
            var context = _userStore.Context as ApplicationDbContext;
            context.Users.Attach(user);
            context.Entry(user).State = EntityState.Modified;
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _userStore.Dispose();
        }
        
        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = _userStore.GetPasswordHashAsync(identityUser);
            SetApplicationUser(user, identityUser);
            return task;
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = _userStore.HasPasswordAsync(identityUser);
            SetApplicationUser(user, identityUser);
            return task;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            var identityUser = ToIdentityUser(user);
            var task = _userStore.SetPasswordHashAsync(identityUser, passwordHash);
            SetApplicationUser(user, identityUser);

            return task;
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = _userStore.GetSecurityStampAsync(identityUser);
            SetApplicationUser(user, identityUser);
            return task;
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
            var identityUser = ToIdentityUser(user);
            var task = _userStore.SetSecurityStampAsync(identityUser, stamp);
            SetApplicationUser(user, identityUser);
            return task;
        }

        private static void SetApplicationUser(ApplicationUser user, IdentityUser identityUser)
        {
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Id = identityUser.Id;
            user.UserName = identityUser.UserName;
        }

        private IdentityUser ToIdentityUser(ApplicationUser user)
        {
            return new IdentityUser
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                UserName = user.UserName
            };
        }
    }
}