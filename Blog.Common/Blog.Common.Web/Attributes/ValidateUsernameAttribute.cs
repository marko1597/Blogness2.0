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
                if (value == null)
                {
                    ErrorMessage = "Model property is null/empty.";
                    return false;
                }

                if (value.GetType() != typeof (string))
                {
                    ErrorMessage ="Model property is not a string.";
                    return false;
                }

                var username = value.ToString();
                if (!string.IsNullOrEmpty(username))
                {
                    var result = IsValidUsername(username);
                    if (result)
                    {
                        ErrorMessage = string.Empty;
                        return true;
                    }

                    ErrorMessage = "Username is already in use";
                    return false;
                }

                ErrorMessage = "Model property is null/empty.";
                return false;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
        
        private bool IsValidUsername(string username)
        {
            var blogUser = UsersResource.GetByUserName(username);

            if (blogUser == null) return false;
            return blogUser.Error != null && blogUser.Error.Id == (int)Utils.Constants.Error.RecordNotFound;
        }
    }
}
