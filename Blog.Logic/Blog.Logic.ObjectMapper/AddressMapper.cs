using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class AddressMapper
    {
        public static Address ToDto(Db.Address address)
        {
            return address == null ? null :
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

        public static Db.Address ToEntity(Address address)
        {
            return address == null ? null :
                new Db.Address
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
