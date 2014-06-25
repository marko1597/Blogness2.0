﻿ngComments.directive('commentItem', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsHubService, commentsService) {
        $scope.canExpandComment = function () {
            if ($scope.comment.Comments == undefined || $scope.comment.Comments == null || $scope.comment.Comments.length < 1) {
                return false;
            }
            return true;
        };

        $scope.toggleReplies = function() {
            $scope.comment.ShowReplies = !$scope.comment.ShowReplies;
        };

        $scope.isExpanded = function() {
            if ($scope.comment.ShowReplies) {
                return "fa-minus";
            }
            return "fa-plus";
        };

        $scope.canReplyToComment = function () {
            if ($scope.comment.PostId == undefined || $scope.comment.PostId == null) {
                return "hidden";
            }
            return "";
        };

        $scope.showAddReply = function() {
            $scope.comment.ShowAddReply = true;
        };

        $scope.isUserLiked = function () {
            var isLiked = false;
            _.each($scope.comment.CommentLikes, function (c) {
                if (c.UserId == $scope.user.UserId) {
                    isLiked = true;
                }
            });

            return isLiked ? "fa-star" : "fa-star-o";
        };

        $scope.isFromPostOwner = function () {
            if ($scope.comment.User.UserName == $scope.poster) {
                return "";
            }
            return "hidden";
        };

        $scope.likeComment = function () {
            commentsService.likeComment($scope.comment.CommentId, $scope.user.UserName);
        };

        $scope.$on("commentLikesUpdate", function (e, d) {
            if ($scope.comment.CommentId == d.CommentId) {
                $scope.comment.CommentLikes = d.CommentLikes;
                $scope.$apply();
                $(".comment-likes-count[data-comment-id='" + d.CommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                $scope.isUserLiked();
            }
        });

        $rootScope.$on("hideAddReply", function () {
            $scope.comment.ShowAddReply = false;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsHubService", "commentsService"];

    return {
        restrict: 'EA',
        scope: {
            comment: '=',
            user: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentItem.html",
        controller: ctrlFn
    };
}]);