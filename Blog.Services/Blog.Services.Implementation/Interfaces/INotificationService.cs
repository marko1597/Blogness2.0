using System.ServiceModel;
using Blog.Common.Contracts.ViewModels.SocketViewModels;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface INotificationService : IBaseService
    {
        [OperationContract]
        void PublishCommentAdded(CommentAdded commentAdded);

        [OperationContract]
        void PublishCommentLikesUpdate(CommentLikesUpdate commentLikesUpdate);

        [OperationContract]
        void PublishPostLikesUpdate(PostLikesUpdate postLikesUpdate);

        [OperationContract]
        void PublishMessage(string message);
    }
}
