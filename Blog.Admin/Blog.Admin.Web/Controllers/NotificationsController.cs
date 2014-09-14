using System.Collections.Generic;
using System.Web.Mvc;
using Blog.Admin.Web.Models.Notifications;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Admin.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationResource _notificationResource;

        public NotificationsController(INotificationResource notificationResource)
        {
            _notificationResource = notificationResource;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(NotificationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(ModelState, JsonRequestBehavior.AllowGet);
            }

            switch (model.Type)
            {
                case "CommentAdded":
                    {
                        var data = new CommentAdded
                                   {
                                       ClientFunction = Constants.SocketClientFunctions.CommentAdded.ToString(),
                                       PostId = model.ChannelId ?? 1,
                                       Comment = new Comment { Id = 99, PostId = model.ChannelId ?? 1, CommentMessage = "Foo bar" }
                                   };
                        _notificationResource.PublishCommentAdded(data);
                    }
                    break;
                case "CommentLikesUpdate":
                    {
                        var data = new CommentLikesUpdate
                        {
                            ClientFunction = Constants.SocketClientFunctions.CommentLikesUpdate.ToString(),
                            CommentId = 1,
                            PostId = model.ChannelId ?? 1,
                            CommentLikes = new List<CommentLike>
                                           {
                                               new CommentLike { CommentId = 1, UserId = 1},
                                               new CommentLike { CommentId = 1, UserId = 2},
                                           }
                        };
                        _notificationResource.PublishCommentLikesUpdate(data);
                    }
                    break;
                case "PostLikesUpdate":
                    {
                        var data = new PostLikesUpdate
                        {
                            ClientFunction = Constants.SocketClientFunctions.PostLikesUpdate.ToString(),
                            PostId = model.ChannelId ?? 1,
                            PostLikes = new List<PostLike>
                                           {
                                               new PostLike { PostId = model.ChannelId ?? 1, UserId = 1},
                                               new PostLike { PostId = model.ChannelId ?? 1, UserId = 2},
                                           }
                        };
                        _notificationResource.PublishPostLikesUpdate(data);
                    }
                    break;
                default:
                    _notificationResource.PublishMessage(model.Message);
                    break;
            }

            return Json(new { Message = "Success" }, JsonRequestBehavior.AllowGet);
        }
    }
}