ngPosts.directive('postItem', [function () {
    var ctrlFn = function ($scope) {
        $scope.post = $scope.data.Post;
        $scope.user = $scope.data.Post.User;
        
        $scope.getCommentPopover = function(commentId) {
            var comment = _.where($scope.comments, { CommentId: commentId });
            var user = comment.User.FirstName + " " + comment.User.LastName;
            return { "title": user, "content": comment.CommentMessage };
        };

        $scope.getPostSize = function() {
            return $scope.data.Width;
        };
    };
    ctrlFn.$inject = ["$scope"];
    
    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postListItem.html",
        controller: ctrlFn
    };
}]);
