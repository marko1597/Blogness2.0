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

        $scope.canExpandComment = function (comment) {
            if (comment.Comments == undefined || comment.Comments == null || comment.Comments.length < 1) {
                return "hidden";
            }
            return "";
        };

        $scope.canReplyToComment = function (comment) {
            if (comment.PostId == undefined || comment.PostId == null) {
                return "hidden";
            }
            return "";
        };

        $scope.showEmptyCommentsMessage = function (comments) {
            if (comments.length != 0) {
                return "hidden";
            }
            return "";
        };

        $scope.isUserLiked = function (comment) {
            var isLiked = false;
            _.each(comment.CommentLikes, function (c) {
                if (c.UserId == $scope.user.UserId) {
                    isLiked = true;
                }
            });

            return isLiked ? "fa-star" : "fa-star-o";
        };

        $scope.likeComment = function (commentId) {
            commentsService.likeComment(commentId, $scope.user.UserName);
        };

        $scope.getComments();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsHubService", "commentsService", "userService", "blockUiService", "errorService"];

    var linkFn = function (scope, elem) {
        scope.$on("commentLikesUpdate", function (e, d) {
            var comment = _.where(scope.comments, { CommentId: d.CommentId })[0];
            comment.CommentLikes = d.CommentLikes;
            scope.$apply();
            $(elem).find("[data-comment-id='" + d.CommentId + "']")
                .find(".comment-header")
                .find(".comment-likes-count")
                .effect("highlight", { color: "#B3C833" }, 1500);
            scope.isUserLiked(comment);
        });
    };

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            postid: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentsList.html",
        controller: ctrlFn,
        link: linkFn
    };
}]);
