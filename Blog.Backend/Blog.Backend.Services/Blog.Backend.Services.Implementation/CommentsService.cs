﻿using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
{
    public class CommentsService : IComments
    {
        public List<Comment> GetByPostId(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().GetByPostId(id);
        }

        public List<Comment> GetByUser(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().GetByUser(id);
        }

        public List<Comment> GetReplies(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().GetReplies(id);
        }

        public bool Add(Comment comment)
        {
            return CommentsFactory.GetInstance().CreateComments().Add(comment);
        }

        public bool Delete(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().Delete(id);
        }
    }
}
