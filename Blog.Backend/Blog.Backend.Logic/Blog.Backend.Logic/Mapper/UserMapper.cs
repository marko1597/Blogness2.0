using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Logic.Mapper
{
    public static class UserMapper
    {
        public static User ToDto(DataAccess.Entities.Objects.User user, bool loadChildren)
        {
            if (user != null)
            {
                if (loadChildren)
                {
                    var education = user.Education != null
                    ? user.Education.Select(EducationMapper.ToDto).ToList()
                    : null;
                    var hobbies = user.Hobbies != null
                        ? user.Hobbies.Select(HobbyMapper.ToDto).ToList()
                        : null;
                    var userPicture = user.UserPictureId != null
                        ? MediaFactory.GetInstance().CreateMedia().Get((int)user.UserPictureId)
                        : null;
                    var userBackground = user.UserBackgroundId != null
                        ? MediaFactory.GetInstance().CreateMedia().Get((int)user.UserBackgroundId)
                        : null;

                    return new User
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        Password = user.Password,
                        BirthDate = user.BirthDate,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailAddress = user.EmailAddress,
                        Address = AddressMapper.ToDto(user.Address),
                        UserPicture = userPicture,
                        UserBackground = userBackground,
                        Education = education,
                        Hobbies = hobbies
                    };
                }

                return new User
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    BirthDate = user.BirthDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    Address = AddressMapper.ToDto(user.Address),
                };
            }
            return new User();
        }

        public static DataAccess.Entities.Objects.User ToEntity(User user)
        {
            if (user != null)
            {
                var education = user.Education != null
                    ? user.Education.Select(EducationMapper.ToEntity).ToList()
                    : null;
                var hobbies = user.Hobbies != null
                    ? user.Hobbies.Select(HobbyMapper.ToEntity).ToList()
                    : null;

                return new DataAccess.Entities.Objects.User
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    BirthDate = user.BirthDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    UserPictureId = user.UserPicture != null ? (int?)user.UserPicture.MediaId : null,
                    UserBackgroundId = user.UserBackground != null ? (int?)user.UserBackground.MediaId : null,
                    Address = user.Address != null ? AddressMapper.ToEntity(user.Address) : null,
                    Education = education,
                    Hobbies = hobbies
                };
            }
            return new DataAccess.Entities.Objects.User();
        }
    }
}
