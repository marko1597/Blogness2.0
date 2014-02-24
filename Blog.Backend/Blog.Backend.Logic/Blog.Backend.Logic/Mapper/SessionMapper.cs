using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class SessionMapper
    {
        public static Session ToDto(DataAccess.Entities.Objects.Session session)
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

        public static DataAccess.Entities.Objects.Session ToEntity(Session session)
        {
            return session == null ?
                new DataAccess.Entities.Objects.Session() :
                new DataAccess.Entities.Objects.Session
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
