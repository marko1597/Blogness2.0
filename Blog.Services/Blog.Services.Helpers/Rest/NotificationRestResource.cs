using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    public class NotificationRestResource : INotificationResource
    {
        public void PublishCommentAdded(CommentAdded commentAdded)
        {
            using (var svc = new ServiceProxyHelper<INotificationService>("NotificationService"))
            {
                svc.Proxy.PublishCommentAdded(commentAdded);
            }
        }

        public void PublishCommentLikesUpdate(CommentLikesUpdate commentLikesUpdate)
        {
            using (var svc = new ServiceProxyHelper<INotificationService>("NotificationService"))
            {
                svc.Proxy.PublishCommentLikesUpdate(commentLikesUpdate);
            }
        }

        public void PublishPostLikesUpdate(PostLikesUpdate postLikesUpdate)
        {
            using (var svc = new ServiceProxyHelper<INotificationService>("NotificationService"))
            {
                svc.Proxy.PublishPostLikesUpdate(postLikesUpdate);
            }
        }

        public void PublishMessage(string message)
        {
            using (var svc = new ServiceProxyHelper<INotificationService>("NotificationService"))
            {
                svc.Proxy.PublishMessage(message);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<INotificationService>("NotificationService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
