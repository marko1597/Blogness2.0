using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    public class NotificationRestResource : INotificationRestResource
    {
        public bool GetHeartBeat()
        {
            throw new System.NotImplementedException();
        }

        public void PublishCommentAdded(CommentAdded commentAdded)
        {
            throw new System.NotImplementedException();
        }

        public void PublishCommentLikesUpdate(CommentLikesUpdate commentLikesUpdate)
        {
            throw new System.NotImplementedException();
        }

        public void PublishPostLikesUpdate(PostLikesUpdate postLikesUpdate)
        {
            throw new System.NotImplementedException();
        }

        public void PublishMessage(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
