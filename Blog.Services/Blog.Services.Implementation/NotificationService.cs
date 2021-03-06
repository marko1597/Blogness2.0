﻿using System.ServiceModel.Activation;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class NotificationService : BaseService, INotificationService
    {
        private readonly IRedisService _redisService;

        public NotificationService(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public void PublishCommentAdded(CommentAdded commentAdded)
        {
            _redisService.Publish(commentAdded);
        }

        public void PublishCommentLikesUpdate(CommentLikesUpdate commentLikesUpdate)
        {
            _redisService.Publish(commentLikesUpdate);
        }

        public void PublishPostLikesUpdate(PostLikesUpdate postLikesUpdate)
        {
            _redisService.Publish(postLikesUpdate);
        }

        public void PublishMessage(string message)
        {
            _redisService.Publish(message);
        }
    }
}
