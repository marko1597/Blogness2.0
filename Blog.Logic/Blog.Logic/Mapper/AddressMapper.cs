using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class AddressMapper
    {
        public static Address ToDto(DataAccess.Database.Entities.Objects.Address address)
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

        public static DataAccess.Database.Entities.Objects.Address ToEntity(Address address)
        {
            return address == null ?
                new DataAccess.Database.Entities.Objects.Address() :
                new DataAccess.Database.Entities.Objects.Address
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
