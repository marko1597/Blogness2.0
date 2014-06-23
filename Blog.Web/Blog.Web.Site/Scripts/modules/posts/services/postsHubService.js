ngPosts.factory("postsHubService", ["$rootScope", "signalrHub",
    function ($rootScope, signalrHub) {
        var hub = new signalrHub("/blog/signalr", "postsHub", {
            postLikesUpdate: function (postId, postLikes) {
                $rootScope.$broadcast("postLikesUpdate", { PostId: postId, PostLikes: postLikes });
            }
        }, []);

        return {
            viewPost: function (postId) {
                hub.viewPost(postId);
            }
        };
    }
]);