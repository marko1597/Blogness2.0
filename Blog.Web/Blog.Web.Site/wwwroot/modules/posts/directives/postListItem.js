ngPosts.directive('postListItem', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $location, $interval, localStorageService, configProvider) {

        $scope.post = $scope.data.Post;

        $scope.user = $scope.data.Post.User;

        $scope.comments = $scope.data.Post.Comments && $scope.data.Post.Comments.length > 0 ? 
            $scope.data.Post.Comments : [];

        $scope.postLikes = $scope.data.Post.PostLikes && $scope.data.Post.PostLikes.length > 0 ?
            $scope.data.Post.PostLikes : [];

        $scope.username = localStorageService.get("username");

        $scope.hasComments = $scope.data.Post.Comments && $scope.data.Post.Comments.length > 0 ? true : false;

        $scope.hasTags = $scope.data.Post.Tags && $scope.data.Post.Tags.length > 0 ? true : false;

        $scope.isEditable = ($scope.user && $scope.user.UserName === $scope.username) ? true : false;
        
        $scope.getCommentPopover = function(commentId) {
            var comment = _.where($scope.comments, { CommentId: commentId });
            var user = comment.User.FirstName + " " + comment.User.LastName;
            return { "title": user, "content": comment.CommentMessage };
        };

        $scope.getPostSize = function() {
            return $scope.data.Width ? '' : $scope.data.Width;
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
        scope: { data: '=' },
        replace: true,
        template: $templateCache.get("posts/postListItem.html"),
        controller: ctrlFn
    };
}]);
