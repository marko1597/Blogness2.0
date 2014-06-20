ngComments.directive('commentsList', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsHubService, commentsService, userService, blockUiService, errorService) {
        $scope.comments = [];

        $scope.getComments = function () {
            blockUiService.blockIt({ elem: ".comment-items-list"});
            commentsService.getCommentsByPost($scope.postid).then(function (comments) {
                $scope.comments = comments;
                blockUiService.unblockIt(".comment-items-list");
            }, function (e) {
                errorService.displayErrorRedirect({ Message: e });
                blockUiService.unblockIt(".comment-items-list");
            });
        };

        $scope.showEmptyCommentsMessage = function (comments) {
            if (comments.length != 0) {
                return "hidden";
            }
            return "";
        };

        $scope.getComments();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsHubService", "commentsService", "userService", "blockUiService", "errorService"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            postid: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentsList.html",
        controller: ctrlFn
    };
}]);
