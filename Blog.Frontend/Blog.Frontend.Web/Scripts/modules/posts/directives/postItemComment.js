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
        template:
            '<div data-comment-id="{{comment.CommentId}}">' +
                '<p><a href="{{user.url}}">{{user.name}}</a></p>' +
                '<p data-pause-trigger data-placement="top-right" data-animation="am-flip-x" bs-popover="popover">{{comment.CommentMessage}}</p>' +
            '</div>',
        controller: ctrlFn
    };
}]);
