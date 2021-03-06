﻿using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class CommentLikesService : BaseService, ICommentLikesService
    {
        private readonly ICommentsLogic _commentsLogic;
        private readonly ICommentLikesLogic _commentLikesLogic;
        private readonly IRedisService _redisService;

        public CommentLikesService(ICommentLikesLogic commentLikesLogic, ICommentsLogic commentsLogic, IRedisService redisService)
        {
            _commentLikesLogic = commentLikesLogic;
            _commentsLogic = commentsLogic;
            _redisService = redisService;
        }

        public List<CommentLike> Get(int commentId)
        {
            return _commentLikesLogic.Get(commentId);
        }

        public void Add(CommentLike commentLike)
        {
            var result = _commentLikesLogic.Add(commentLike);
            if (result != null && result.Error != null) throw new Exception(result.Error.Message);

            var parentComment = _commentsLogic.Get(commentLike.CommentId);
            if (parentComment == null) throw new Exception(string.Format(
                "Failed to like comment {0} due to error in fetching comment", commentLike.CommentId));

            var commentLikes = _commentLikesLogic.Get(commentLike.CommentId);
            if (commentLikes == null) throw new Exception(string.Format(
                "Failed to like comment {0} due to error in fetching likes in comment", commentLike.CommentId));

            var commentLikesUpdate = new CommentLikesUpdate
            {
                CommentId = commentLike.CommentId,
                PostId = parentComment.PostId,
                CommentLikes = commentLikes,
                ClientFunction = Constants.SocketClientFunctions.CommentLikesUpdate.ToString()
            };

            _redisService.Publish(commentLikesUpdate);
        }
    }
}
