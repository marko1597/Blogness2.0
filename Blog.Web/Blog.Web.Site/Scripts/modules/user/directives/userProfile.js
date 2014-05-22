ngUser.directive('userProfile', [function () {
    var ctrlFn = function ($scope) {
        $scope.user = {
            "name": $scope.comment.User.FirstName + " " + $scope.comment.User.LastName,
            "url": "#"
        };

    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { user: '=' },
        replace: true,
        template:
            '<div data-user-id="{{user.UserId}}">' +
                '<p><a href="{{user.url}}">{{user.name}}</a></p>' +
                '<p data-pause-trigger data-placement="top-right" data-animation="am-flip-x" bs-popover="popover">{{comment.CommentMessage}}</p>' +
            '</div>',
        controller: ctrlFn
    };
}]);
