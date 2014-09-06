ngPosts.factory("postsHubService", ["$rootScope", "$interval", "Hub", "configProvider",
    function ($rootScope, $interval, Hub, configProvider) {
        var hub = new Hub("postsHub", {
            listeners: {
                postLikesUpdate: function (postId, postLikes) {
                    $rootScope.$broadcast("postLikesUpdate", { PostId: postId, PostLikes: postLikes });
                }
            },
            methods: ["viewPost"]
        });

        var stop;

        return {
            viewPost: function (postId) {
                stop = $interval(function () {
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