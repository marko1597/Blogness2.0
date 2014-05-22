ngPosts.factory("postsHubService", ["$rootScope", "signalrHub",
    function ($rootScope, signalrHub) {
        var hub = new signalrHub("/blog/signalr", "postsHub", {
            postsLikeUpdate: function (postId, postLikes) {
                $rootScope.$broadcast("postsLikeUpdate", { PostId: postId, PostLikes: postLikes });
            }
        }, []);

        return {};
    }
]);