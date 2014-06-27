ngComments.directive('commentsList', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsHubService, commentsService, userService, blockUiService, errorService) {
        $scope.comments = [];

        $scope.getComments = function () {
            blockUiService.blockIt({ elem: ".comment-items-list"});
            commentsService.getCommentsByPost($scope.postid).then(function (comments) {
                $scope.comments = comments;
                commentsHubService.viewPost($scope.postid);
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

        $scope.$on("commentAdded", function (e, d) {
            if (d.Comment.PostId != null || d.Comment.PostId != undefined) {
                $scope.comments.unshift(d.Comment);
                $scope.$apply();
                $(".comment-item[data-comment-id='" + d.Comment.CommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
            } else {
                _.each($scope.comments, function (comment) {
                    if (comment.CommentId == d.Comment.ParentCommentId) {
                        comment.Comments.unshift(d.Comment);
                        $scope.$apply();
                        $(".comment-item[data-comment-id='" + d.Comment.CommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        $(".comment-item[data-comment-id='" + d.Comment.ParentCommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        return;
                    }
                });
            }
        });

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
