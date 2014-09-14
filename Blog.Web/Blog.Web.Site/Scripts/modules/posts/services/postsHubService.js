ngPosts.factory("postsHubService", [function() {}]);
//ngPosts.factory("postsHubService", ["$rootScope", "$interval", "Hub",
//    function ($rootScope, $interval, Hub) {
//        var hub = new Hub("postsHub", {
//            listeners: {
//                postLikesUpdate: function (postId, postLikes) {
//                    $rootScope.$broadcast("postLikesUpdate", { PostId: postId, PostLikes: postLikes });
//                }
//            },
//            methods: ["viewPost"],
//            logging: true
//        });

//        var stop;

//        return {
//            viewPost: function (postId) {
//                stop = $interval(function () {
//                    if (hub.isConnected) {
//                        hub.viewPost(postId);
//                        if (angular.isDefined(stop)) {
//                            $interval.cancel(stop);
//                            stop = undefined;
//                        }
//                    }
//                }, 100);

//            }
//        };
//    }
//]);