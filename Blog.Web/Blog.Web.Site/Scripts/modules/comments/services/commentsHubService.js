ngComments.factory("commentsHubService", ["$rootScope", "signalrHub",
    function ($rootScope, signalrHub) {
        var hub = new signalrHub("/blog/signalr", "commentsHub", {
            commentLikesUpdate: function (commentId, commentLikes) {
                $rootScope.$broadcast("commentLikesUpdate", { CommentId: commentId, CommentLikes: commentLikes });
            }
        }, []);

        return {};
    }
]);