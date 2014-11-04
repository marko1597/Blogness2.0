using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class ChatMessageMapper
    {
        public static ChatMessage ToDto(Db.ChatMessage chatMessage)
        {
            if (chatMessage == null) return null;
            if (chatMessage.FromUser == null || chatMessage.ToUser == null) return null;

            return new ChatMessage
            {
                Id = chatMessage.ChatMessageId,
                FromUser = UserMapper.ToDto(chatMessage.FromUser),
                ToUser = UserMapper.ToDto(chatMessage.ToUser),
                Text = chatMessage.Text,
                CreatedBy = chatMessage.CreatedBy,
                CreatedDate = chatMessage.CreatedDate,
                ModifiedBy = chatMessage.ModifiedBy,
                ModifiedDate = chatMessage.ModifiedDate
            };
        }

        public static Db.ChatMessage ToEntity(ChatMessage chatMessage)
        {
            if (chatMessage == null) return null;
            if (chatMessage.FromUser == null || chatMessage.ToUser == null) return null;

            return new Db.ChatMessage
            {
                FromUser = UserMapper.ToEntity(chatMessage.FromUser),
                FromUserId = chatMessage.FromUser.Id,
                ToUser = UserMapper.ToEntity(chatMessage.ToUser),
                ToUserId = chatMessage.ToUser.Id,
                Text = chatMessage.Text,
                CreatedBy = chatMessage.CreatedBy,
                CreatedDate = chatMessage.CreatedDate,
                ModifiedBy = chatMessage.ModifiedBy,
                ModifiedDate = chatMessage.ModifiedDate
            };
        }
    }
}
