ngPosts.directive('postListItem', [function () {
    var ctrlFn = function ($scope, $location, localStorageService) {
        
        $scope.post = $scope.data.Post;

        $scope.user = $scope.data.Post.User;

        $scope.username = localStorageService.get("username");

        $scope.hasComments = $scope.data.Post.Comments.length > 0 ? true : false;

        $scope.hasTags = $scope.data.Post.Tags.length > 0 ? true : false;
        
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
            $location.path("/post/edit/" + $scope.post.Id);
        };
    };
    ctrlFn.$inject = ["$scope", "$location", "localStorageService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postListItem.html",
        controller: ctrlFn
    };
}]);
