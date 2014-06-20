ngComments.directive('commentsAddNew', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsService, blockUiService, errorService) {
        $scope.comment = {
            CommentMessage: "",
            PostId: $scope.postid,
            ParentCommentId: $scope.commentid,
            User: $scope.user
        };

        $scope.hideAddComment = function () {
            if ($scope.commentid == undefined || $scope.commentid == null) {
                $rootScope.$broadcast("hideAddComment");
            } else {
                $rootScope.$broadcast("hideAddReply");
            }
        };

        $scope.saveComment = function () {
            blockUiService.blockIt({ elem: ".comment-item-new" });
            commentsService.addComment($scope.comment).then(function (resp) {
                if (resp.Error == undefined) {
                    blockUiService.unblockIt(".comment-item-new");
                } else {
                    blockUiService.unblockIt(".comment-item-new");
                    errorService.displayError(resp.Error);
                }
            }, function (e) {
                blockUiService.unblockIt();
                errorService.displayErrorRedirect(e);
            });
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsService", "blockUiService", "errorService"];

    return {
        restrict: 'EA',
        scope: {
            commentid: '=',
            postid: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentsAddNew.html",
        controller: ctrlFn
    };
}]);
