ngPosts.directive('postItemComment', [function () {
    var ctrlFn = function ($scope) {
        $scope.user = {
            "name": $scope.comment.User.FirstName + " " + $scope.comment.User.LastName,
            "url": "#"
        };

        $scope.popover = {
            "title": $scope.user.name,
            "content": $scope.comment.CommentMessage
        };
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { comment: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "posts/postListItemComment.html",
        controller: ctrlFn
    };
}]);
