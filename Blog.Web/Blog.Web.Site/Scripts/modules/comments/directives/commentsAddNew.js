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
            commentsService.addComment($scope.createCommentForAdding()).then(function (resp) {
                if (resp.Error == undefined) {
                    $scope.comment.CommentMessage = "";
                    $scope.hideAddComment();
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

        $scope.createCommentForAdding = function() {
            return {
                PostId: $scope.parentpostid,
                Comment: $scope.comment
            };
        };
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsService", "blockUiService", "errorService"];

    return {
        restrict: 'EA',
        scope: {
            commentid: '=',
            postid: '=',
            user: '=',
            parentpostid: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentsAddNew.html",
        controller: ctrlFn
    };
}]);
