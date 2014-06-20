ngPosts.directive('postLikes', ["$rootScope", "postsHubService", "postsService", "userService",
    function ($rootScope, postsHubService, postsService, userService) {
        var linkFn = function (scope, elem) {
            scope.postId = scope.data.PostId;
            scope.postLikes = scope.data.PostLikes;
            scope.user = {};

            scope.tooltip = {
                "title": "Click to favorite this post.",
            };

            scope.init = function() {
                userService.getUserInfo().then(function(resp) {
                    scope.user = resp;
                });
            };

            scope.$on("postLikesUpdate", function (e, d) {
                if (d.PostId == scope.data.PostId) {
                    scope.postLikes = d.PostLikes;
                    scope.$apply();
                    $(elem).effect("highlight", { color: "#B3C833" }, 1500);
                    scope.isUserLiked();
                }
            });

            scope.$on("viewedPostLoaded", function(e, d) {
                scope.postId = d.PostId;
                scope.postLikes = d.PostLikes;
                scope.isUserLiked();
            });

            scope.likePost = function () {
                postsService.likePost(scope.data.PostId, scope.user.UserName);
            };

            scope.isUserLiked = function() {
                var isLiked = false;
                _.each(scope.postLikes, function(p) {
                    if (p.UserId == scope.user.UserId) {
                        isLiked = true;
                    }
                });

                return isLiked ? "fa-star" : "fa-star-o";
            };

            scope.init();
        };

        return {
            restrict: 'EA',
            scope: { data: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesUrl + "posts/postlikes.html",
            link: linkFn
        };
    }
]);
