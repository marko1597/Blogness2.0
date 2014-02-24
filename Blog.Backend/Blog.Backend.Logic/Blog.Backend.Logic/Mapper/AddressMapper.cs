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
                    UserId = address.UserId
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
                    UserId = address.UserId
                };
        }
    }
}
