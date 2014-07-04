ngPosts.factory("postsHubService", ["$rootScope", "$interval", "signalrHub",
    function ($rootScope, $interval, signalrHub) {
        var hub = new signalrHub("signalr", "postsHub", {
            postLikesUpdate: function (postId, postLikes) {
                $rootScope.$broadcast("postLikesUpdate", { PostId: postId, PostLikes: postLikes });
            }
        }, ["viewPost"]);

        var stop;

        return {
            viewPost: function (postId) {
                stop = $interval(function() {
                    if (hub.isConnected) {
                        hub.viewPost(postId);
                        if (angular.isDefined(stop)) {
                            $interval.cancel(stop);
                            stop = undefined;
                        }
                    }
                }, 100);
                
            }
        };
    }
]);