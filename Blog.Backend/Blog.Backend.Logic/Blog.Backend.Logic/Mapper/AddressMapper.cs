using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class AddressMapper
    {
        public static Address ToDto(DataAccess.Entities.Objects.Address address)
        {
            return address == null ?
                new Address() :
                new Address
                {
                    AddressId = address.AddressId,
                    City = address.City,
                    Country = address.Country,
                    State = address.State,
                    StreetAddress = address.StreetAddress,
                    Zip = address.Zip,
                    User = address.User != null ? UserMapper.ToDto(address.User) : null
                };
        }

        public static DataAccess.Entities.Objects.Address ToEntity(Address address)
        {
            return address == null ?
                new DataAccess.Entities.Objects.Address() :
                new DataAccess.Entities.Objects.Address
                {
                    AddressId = address.AddressId,
                    City = address.City,
                    Country = address.Country,
                    State = address.State,
                    StreetAddress = address.StreetAddress,
                    Zip = address.Zip,
                    User = address.User != null ? UserMapper.ToEntity(address.User) : null,
                    UserId = address.User != null ? address.User.UserId : 0
                };
        }
    }
}
