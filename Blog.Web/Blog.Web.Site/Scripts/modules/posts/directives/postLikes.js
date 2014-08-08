ngPosts.directive('postLikes', ["$rootScope", "postsHubService", "postsService", "userService", "errorService", "localStorageService",
    function ($rootScope, postsHubService, postsService, userService, errorService, localStorageService) {
        var linkFn = function (scope, elem) {
            scope.postId = scope.data.PostId;
            scope.postLikes = scope.data.PostLikes;
            scope.user = {};
            scope.username = localStorageService.get("username");
            scope.authData = localStorageService.get("authorizationData");

            scope.tooltip = {
                "title": "Click to favorite this post.",
            };

            scope.init = function () {
                if (scope.username) {
                    userService.getUserInfo(scope.username).then(function (resp) {
                        scope.user = resp;
                    });
                }
            };

            scope.$on("postLikesUpdate", function (e, d) {
                if (d.PostId == scope.data.PostId) {
                    scope.postLikes = d.PostLikes;
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
                postsService.likePost(scope.data.PostId, scope.user.UserName).then(function() {
                    // TODO: This should call the logger api
                    console.log(scope.user.UserName + " liked post " + scope.data.PostId);
                },
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

            scope.init();
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
