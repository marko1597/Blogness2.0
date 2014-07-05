ngPosts.factory("postsHubService", ["$rootScope", "$interval", "signalrHub", "configProvider",
    function ($rootScope, $interval, signalrHub, configProvider) {
        var hubUrl = configProvider.getSettings().HubUrl == "" ?
            window.blogConfiguration.hubUrl :
            configProvider.getSettings().HubUrl;

        var hub = new signalrHub(hubUrl, "postsHub", {
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