using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class UserMapper
    {
        public static User ToDto(DataAccess.Entities.Objects.User user)
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
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    BirthDate = user.BirthDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    Address = AddressMapper.ToDto(user.Address),
                    Education = education,
                    Hobbies = hobbies
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
                    Address = user.Address != null ? AddressMapper.ToEntity(user.Address) : null,
                    Education = education,
                    Hobbies = hobbies
                };
            }
            return new DataAccess.Entities.Objects.User();
        }
    }
}
