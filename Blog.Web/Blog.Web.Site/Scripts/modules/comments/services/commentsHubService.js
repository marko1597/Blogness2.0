ngComments.factory("commentsHubService", [
    function() {}
]);
//ngComments.factory("commentsHubService", ["$rootScope", "$interval", "Hub", "commentsService",
//    function ($rootScope, $interval, Hub, commentsService) {
//        var hub = new Hub("commentsHub", {
//            listeners: {
//                commentLikesUpdate: function (commentId, commentLikes) {
//                    $rootScope.$broadcast("commentLikesUpdate", { CommentId: commentId, CommentLikes: commentLikes });
//                },
//                commentAdded: function (postId, comment) {
//                    if (comment.PostId != null) {
//                        comment = commentsService.addViewProperties(comment, false, false);
//                    } else {
//                        comment = commentsService.addViewProperties(comment);
//                    }

//                    $rootScope.$broadcast("commentAdded", { PostId: postId, Comment: comment });
//                }
//            },
//            methods: ["viewPost"],
//            logging: true
//        });
//        // TODO: Oh so hackish way! Pleeeeaaaase update it to be better. :(
//        hub.disconnect();
//        hub.connect();

//        var stop;

//        return {
//            viewPost: function (postId) {
//                stop = $interval(function () {
//                    if (hub.connection.state != 0) {
//                        hub.viewPost(postId);
//                        if (angular.isDefined(stop)) {
//                            $interval.cancel(stop);
//                            stop = undefined;
//                        }
//                    }
//                }, 200);
//            }
//        };
//    }
//]);