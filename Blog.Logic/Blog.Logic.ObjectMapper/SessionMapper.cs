using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class SessionMapper
    {
        public static Session ToDto(Db.Session session)
        {
            return session == null ? null : 
                new Session
                {
                    SessionId = session.SessionId,
                    IpAddress = session.IpAddress,
                    UserId = session.UserId,
                    TimeValidity = session.TimeValidity,
                    Token = session.Token
                };
        }

        public static Db.Session ToEntity(Session session)
        {
            return session == null ? null :
                new Db.Session
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
