ngComments.directive('commentsAddNew', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsService, blockUiService, errorService) {
        $scope.comment = {
            CommentMessage: "",
            PostId: $scope.postid,
            ParentCommentId: $scope.commentid,
            User: $scope.user
        };

        $scope.hasError = false;
        $scope.commentMessageStyle = function() {
            if ($scope.hasError) {
                return errorService.highlightField();
            }
            return "";
        };

        $scope.removeCommentMessageError = function() {
            $scope.hasError = false;
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
            if ($scope.comment.CommentMessage != "") {
                commentsService.addComment($scope.createCommentForAdding()).then(function(resp) {
                    if (resp.Error == undefined) {
                        $scope.comment.CommentMessage = "";
                        $scope.hideAddComment();
                        blockUiService.unblockIt(".comment-item-new");
                    } else {
                        $scope.hasError = true;
                        blockUiService.unblockIt(".comment-item-new");
                        errorService.displayError(resp.Error);
                    }
                }, function(e) {
                    blockUiService.unblockIt(".comment-item-new");
                    errorService.displayErrorRedirect(e);
                });
            } else {
                $scope.hasError = true;
                blockUiService.unblockIt(".comment-item-new");
                errorService.displayError({ Message: "Your comment message is empty. Please don't be that stupid." });
            }
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
        templateUrl: window.blogConfiguration.templatesModulesUrl + "comments/commentsAddNew.html",
        controller: ctrlFn
    };
}]);
