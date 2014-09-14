ngPosts.directive('postLikes', ["$rootScope", "postsHubService", "postsService", "userService", "errorService", "localStorageService", "configProvider",
    function ($rootScope, postsHubService, postsService, userService, errorService, localStorageService, configProvider) {
        var linkFn = function (scope, elem) {
            scope.postId = scope.data.PostId;
            scope.postLikes = scope.data.PostLikes;
            scope.user = {};
            scope.username = localStorageService.get("username");
            scope.authData = localStorageService.get("authorizationData");

            scope.tooltip = {
                "title": "Click to favorite this post.",
            };

            scope.$on("loggedInUserInfo", function (ev, data) {
                scope.user = data;
            });

            scope.$on(configProvider.getSocketClientFunctions().postLikesUpdate, function (e, d) {
                if (d.postId == scope.data.PostId) {
                    scope.postLikes = d.postLikes;
                    scope.$apply();
                    $(elem).effect("highlight", { color: "#B3C833" }, 1500);
                    scope.isUserLiked();
                }
            });

            scope.$on("viewedPostLoaded", function (e, d) {
                scope.postId = d.PostId;
                scope.postLikes = d.PostLikes;
                scope.isUserLiked();
            });

            scope.likePost = function () {
                postsService.likePost(scope.data.PostId, scope.username).then(function() {},
                function(err) {
                    errorService.displayError(err);
                });
            };

            scope.isUserLiked = function () {
                var isLiked = false;
                if (scope.authData) {
                    _.each(scope.postLikes, function(p) {
                        if (p.UserId == scope.user.Id) {
                            isLiked = true;
                        }
                    });
                }

                return isLiked ? "fa-star" : "fa-star-o";
            };
        };

        return {
            restrict: 'EA',
            scope: { data: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postlikes.html",
            link: linkFn
        };
    }
]);
