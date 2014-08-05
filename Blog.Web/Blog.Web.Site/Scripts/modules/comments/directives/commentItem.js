ngComments.directive('commentItem', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsHubService, commentsService, errorService) {
        $scope.canExpandComment = function () {
            if ($scope.comment.Comments == undefined || $scope.comment.Comments == null || $scope.comment.Comments.length < 1) {
                return false;
            }
            return true;
        };

        $scope.toggleReplies = function() {
            var state = !$scope.comment.ShowReplies;
            $scope.comment.ShowReplies = state;

            if (!state) {
                $rootScope.$broadcast("hideAddReply");
            }
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
            if ($scope.comment.User.UserName == $scope.poster) {
                return "";
            }
            return "hidden";
        };

        $scope.likeComment = function () {
            commentsService.likeComment($scope.comment.Id, $scope.user.UserName).then(function () {
                // TODO: This should call the logger api
                console.log($scope.user.UserName + " liked comment " + $scope.comment.Id);
            },
                function (err) {
                    errorService.displayError(err);
                });;
        };

        $scope.$on("commentLikesUpdate", function (e, d) {
            if ($scope.comment.Id == d.CommentId) {
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
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsHubService", "commentsService", "errorService"];

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
