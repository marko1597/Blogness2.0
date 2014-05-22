using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class SessionMapper
    {
        public static Session ToDto(DataAccess.Database.Entities.Objects.Session session)
        {
            return session == null ?
                new Session() : 
                new Session
                {
                    SessionId = session.SessionId,
                    IpAddress = session.IpAddress,
                    UserId = session.UserId,
                    TimeValidity = session.TimeValidity,
                    Token = session.Token
                };
        }

        public static DataAccess.Database.Entities.Objects.Session ToEntity(Session session)
        {
            return session == null ?
                new DataAccess.Database.Entities.Objects.Session() :
                new DataAccess.Database.Entities.Objects.Session
                {
                    SessionId = session.SessionId,
                    IpAddress = session.IpAddress,
                    UserId = session.UserId,
                    TimeValidity = session.TimeValidity,
                    Token = session.Token
                };
        }
    }
}
