ngPosts.directive('postItem', [function () {
    var ctrlFn = function ($scope, $location, localStorageService) {
        $scope.post = $scope.data.Post;
        $scope.user = $scope.data.Post.User;
        $scope.username = localStorageService.get("username");
        
        $scope.getCommentPopover = function(commentId) {
            var comment = _.where($scope.comments, { CommentId: commentId });
            var user = comment.User.FirstName + " " + comment.User.LastName;
            return { "title": user, "content": comment.CommentMessage };
        };

        $scope.getPostSize = function() {
            return $scope.data.Width;
        };

        $scope.isEditable = function () {
            if ($scope.user.UserName === $scope.username) {
                return true;
            }
            return false;
        };

        $scope.editPost = function() {
            $location.path("/post/new/" + $scope.post.PostId);
        };
    };
    ctrlFn.$inject = ["$scope", "$location", "localStorageService"];
    
    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postListItem.html",
        controller: ctrlFn
    };
}]);
