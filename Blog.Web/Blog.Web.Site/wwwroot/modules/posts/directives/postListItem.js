ngPosts.directive('postListItem', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $location, $interval, localStorageService, configProvider) {

        $scope.username = localStorageService.get("username");

        $scope.user = $scope.post && $scope.post.User ? $scope.post.User : null;

        $scope.comments = $scope.post && $scope.post.Comments ? $scope.post.Comments : [];

        $scope.postLikes = $scope.post && $scope.post.PostLikes ? $scope.post.PostLikes : [];

        $scope.hasComments = $scope.post && $scope.post.Comments && $scope.post.Comments.length > 0 ? true : false;

        $scope.hasTags = $scope.post && $scope.post.Tags && $scope.post.Tags.length > 0 ? true : false;

        $scope.isEditable = $scope.user && $scope.user.UserName === $scope.username ? true : false;
        
        $scope.getCommentPopover = function(commentId) {
            var comment = _.where($scope.comments, { CommentId: commentId });
            var user = comment.User.FirstName + " " + comment.User.LastName;
            return { "title": user, "content": comment.CommentMessage };
        };

        $scope.getPostSize = function() {
            return $scope.width ? $scope.width : '';
        };

        $scope.toggleIsEditable = function () {
            if ($scope.user && $scope.user.UserName === $scope.username) {
                $scope.isEditable = true;
            }
            $scope.isEditable = false;
        };

        var stop;
        stop = $interval(function () {
            if (configProvider.getSocketClientFunctions().getPostTopComments && configProvider.getSocketClientFunctions().getPostLikes) {
                $rootScope.$on(configProvider.getSocketClientFunctions().getPostTopComments, function (e, d) {
                    if (d.postId == $scope.post.Id) {
                        $scope.comments = d.comments;
                        $scope.hasComments = d.comments && d.comments.length > 0 ? true : false;
                    }
                });

                $rootScope.$on(configProvider.getSocketClientFunctions().getPostLikes, function (e, d) {
                    if (d.postId == $scope.post.Id) {
                        $scope.postLikes = d.postLikes;
                    }
                });

                $interval.cancel(stop);
                stop = undefined;
            }
        }, 250);

        $scope.$on("loggedInUserInfo", function (ev, data) {
            if (data) {
                $scope.username = data.UserName;
                $scope.toggleIsEditable();
            }
        });

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.username = $rootScope.user.UserName;
                $scope.toggleIsEditable();
            }
        });

        $scope.editPost = function() {
            $location.path("/post/edit/" + $scope.post.Id);
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "$interval", "localStorageService", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            post: '=',
            width: '='
        },
        replace: true,
        template: $templateCache.get("posts/postListItem.html"),
        controller: ctrlFn
    };
}]);
