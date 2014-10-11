using System;
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
    public class ViewCountService : BaseService, IViewCountService
    {
        private readonly IViewCountLogic _viewCountLogic;
        private readonly IRedisService _redisService;

        public ViewCountService(IViewCountLogic viewCountLogic, IRedisService redisService)
        {
            _viewCountLogic = viewCountLogic;
            _redisService = redisService;
        }

        public List<ViewCount> Get(int postId)
        {
            return _viewCountLogic.Get(postId);
        }

        public void Add(ViewCount viewCount)
        {
            var result = _viewCountLogic.Add(viewCount);
            if (result != null && result.Error != null) throw new Exception(result.Error.Message);

            var viewCounts = _viewCountLogic.Get(viewCount.PostId);
            var viewCountUpdate = new ViewCountUpdate
            {
                PostId = viewCount.PostId,
                ViewCounts = viewCounts,
                ClientFunction = Constants.SocketClientFunctions.ViewCountUpdate.ToString()
            };

            _redisService.Publish(viewCountUpdate);
        }
    }
}
