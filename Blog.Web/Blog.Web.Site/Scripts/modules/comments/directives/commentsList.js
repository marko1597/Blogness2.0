ngComments.directive('commentsList', [function () {
    var ctrlFn = function ($scope, $rootScope, commentsHubService, commentsService, userService, errorService) {
        $scope.comments = [];

        $scope.emptyCommentsMessage = "";

        $scope.hasError = false;

        $scope.getComments = function () {
            if (!isNaN($scope.postid)) {
                commentsService.getCommentsByPost($scope.postid).then(function (comments) {
                    $scope.hasError = false;
                    $scope.comments = comments;
                    commentsHubService.viewPost($scope.postid);
                }, function(err) {
                    $scope.hasError = true;
                    errorService.displayError(err);
                });
            } else {
                $scope.hasError = true;
            }
        };

        $scope.showEmptyCommentsMessage = function () {
            if ($scope.comments.length != 0) {
                return false;
            }
            return true;
        };

        $scope.emptyMessageStyle = function() {
            return $scope.hasError ? "alert-danger" : "alert-warning";
        };

        $scope.getEmptyCommentsMessage = function() {
            return $scope.hasError ?
                "Something went wrong with loading the comments! :(" :
                "There are no comments yet.";
        };

        $scope.$on("commentAdded", function (e, d) {
            if (d.Comment.PostId != null || d.Comment.PostId != undefined) {
                $scope.comments.unshift(d.Comment);
                $scope.$apply();
                $(".comment-item[data-comment-id='" + d.Comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
            } else {
                _.each($scope.comments, function (comment) {
                    if (comment.Id == d.Comment.ParentCommentId) {
                        comment.Comments.unshift(d.Comment);
                        $scope.$apply();
                        $(".comment-item[data-comment-id='" + d.Comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        $(".comment-item[data-comment-id='" + d.Comment.ParentCommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        return;
                    }
                });
            }
        });

        $scope.getComments();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsHubService", "commentsService", "userService", "errorService"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            postid: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "comments/commentsList.html",
        controller: ctrlFn
    };
}]);
