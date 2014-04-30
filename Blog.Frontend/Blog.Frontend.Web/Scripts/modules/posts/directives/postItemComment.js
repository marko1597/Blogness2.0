postsModule.directive('postItemComment', ["$popover", function ($popover) {
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

    var linkFn = function(scope, elem) {
        $(elem).find("p:last-child").on("click", function () {
            var pop = $(elem).find("div.popover");
            if (pop.length > 0) {
                $(elem).closest("div.post-item-comments ul").newsTicker('unpause');
            } else {
                $(elem).closest("div.post-item-comments ul").newsTicker('pause');
            }
        });
    };

    return {
        restrict: 'EA',
        scope: { comment: '=' },
        replace: true,
        link: linkFn,
        template:
            '<div>' +
                '<p><a href="{{user.url}}">{{user.name}}</a></p>' +
                '<p data-pause-trigger data-placement="top-right" data-animation="am-flip-x" bs-popover="popover">{{comment.CommentMessage}}</p>' +
            '</div>',
        controller: ctrlFn
    };
}]);
