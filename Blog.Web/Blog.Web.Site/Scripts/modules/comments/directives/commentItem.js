ngComments.directive('commentItem', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsService, errorService, configProvider) {
        $scope.canExpandComment = function () {
            if ($scope.comment.Comments == undefined || $scope.comment.Comments === null || $scope.comment.Comments.length < 1) {
                return false;
            }
            return true;
        };

        $scope.toggleReplies = function () {
            var state = !$scope.comment.ShowReplies;
            $scope.comment.ShowReplies = state;

            if (!state) {
                $rootScope.$broadcast("hideAddReply");
            }
        };

        $scope.isExpanded = function () {
            if ($scope.comment.ShowReplies) {
                return "fa-minus";
            }
            return "fa-plus";
        };

        $scope.canReplyToComment = function () {
            if ($scope.comment.PostId == undefined || $scope.comment.PostId === null) {
                return "hidden";
            }
            return "";
        };

        $scope.showAddReply = function () {
            $scope.comment.ShowAddReply = true;

            if (!$scope.comment.ShowReplies) {
                $scope.toggleReplies();
                $scope.isExpanded();
            }
        };

        $scope.isUserLiked = function () {
            var isLiked = false;
            _.each($scope.comment.CommentLikes, function (c) {
                if (c.UserId == $scope.user.Id) {
                    isLiked = true;
                }
            });

            return isLiked ? "fa-star" : "fa-star-o";
        };

        $scope.isFromPostOwner = function () {
            if ($scope.comment.User != null && $scope.comment.User.UserName == $scope.poster) {
                return "";
            }
            return "hidden";
        };

        $scope.likeComment = function () {
            commentsService.likeComment($scope.comment.Id, $scope.user.UserName).then(function () { },
                function (err) {
                    errorService.displayError(err);
                });;
        };

        $scope.$on(configProvider.getSocketClientFunctions().commentLikesUpdate, function (e, d) {
            if ($scope.comment.Id == d.commentId) {
                $scope.comment.CommentLikes = d.commentLikes;
                $scope.$apply();
                $(".comment-likes-count[data-comment-id='" + d.commentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                $scope.isUserLiked();
            }
        });

        $rootScope.$on("hideAddReply", function () {
            $scope.comment.ShowAddReply = false;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsService", "errorService", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            comment: '=',
            user: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "comments/commentItem.html",
        controller: ctrlFn
    };
}]);
