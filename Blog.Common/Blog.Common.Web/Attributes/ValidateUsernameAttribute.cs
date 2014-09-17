using System;
using System.ComponentModel.DataAnnotations;
using Blog.Common.Utils.Extensions;
using Blog.Services.Helpers.Wcf;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Common.Web.Attributes
{
    public class ValidateUsernameAttribute : ValidationAttribute
    {
        private IUsersResource _usersResource;
        public IUsersResource UsersResource
        {
            get
            {
                return _usersResource ?? new UsersResource();
            }
            set { _usersResource = value; }
        }

        public override bool IsValid(object value)
        {
            try
            {
                if (value.GetType() != typeof(string)) throw new Exception("Model property is not a string.");

                var username = value.ToString();
                if (string.IsNullOrEmpty(username)) throw new Exception("Model property is null/empty.");

                return IsValidUsername(username);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
        
        private bool IsValidUsername(string username)
        {
            var blogUser = _usersResource.GetByUserName(username);

            if (blogUser == null) return false;
            return blogUser.Error != null && blogUser.Error.Id == (int)Utils.Constants.Error.RecordNotFound;
        }
    }
}
