using System.Linq;
using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class UserMapper
    {
        public static User ToDto(Db.User user)
        {
            if (user != null)
            {
                var education = user.Education != null
                    ? user.Education.Select(EducationMapper.ToDto).ToList()
                    : null;
                var hobbies = user.Hobbies != null
                    ? user.Hobbies.Select(HobbyMapper.ToDto).ToList()
                    : null;

                return new User
                {
                    Id = user.UserId,
                    UserName = user.UserName,
                    IdentityId = user.IdentityId,
                    BirthDate = user.BirthDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    Address = AddressMapper.ToDto(user.Address),
                    Education = education,
                    Hobbies = hobbies,
                    PictureId = user.PictureId,
                    BackgroundId = user.BackgroundId,
                    IsDeleted = user.IsDeleted
                };
            }
            return new User();
        }

        public static Db.User ToEntity(User user)
        {
            if (user != null)
            {
                var education = user.Education != null
                    ? user.Education.Select(EducationMapper.ToEntity).ToList()
                    : null;
                var hobbies = user.Hobbies != null
                    ? user.Hobbies.Select(HobbyMapper.ToEntity).ToList()
                    : null;
                var picture = user.Picture != null
                    ? (int?)user.Picture.Id
                    : null;
                var background = user.Background != null
                    ? (int?)user.Background.Id
                    : null;

                return new Db.User
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IdentityId = user.IdentityId,
                    BirthDate = user.BirthDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    Address = user.Address != null ? AddressMapper.ToEntity(user.Address) : null,
                    Education = education,
                    Hobbies = hobbies,
                    PictureId = picture,
                    BackgroundId = background,
                    IsDeleted = user.IsDeleted
                };
            }
            return null;
        }
    }
}
