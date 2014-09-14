ngComments.directive('commentsList', [function () {
    var ctrlFn = function ($scope, $rootScope, postsService, commentsService, userService, errorService, configProvider) {
        $scope.comments = [];

        $scope.emptyCommentsMessage = "";

        $scope.hasError = false;

        $scope.getComments = function () {
            if (!isNaN($scope.postid)) {
                commentsService.getCommentsByPost($scope.postid).then(function (comments) {
                    $scope.hasError = false;
                    $scope.comments = comments;
                    postsService.subscribeToPost($scope.postid);
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

        $scope.$on(configProvider.getSocketClientFunctions().commentAdded, function (e, d) {
            if (d.postId != null || d.postId != undefined) {
                $scope.comments.unshift(d.comment);
                $scope.$apply();
                $(".comment-item[data-comment-id='" + d.comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
            } else {
                _.each($scope.comments, function (comment) {
                    if (comment.Id == d.cmment.ParentCommentId) {
                        comment.Comments.unshift(d.comment);
                        $scope.$apply();
                        $(".comment-item[data-comment-id='" + d.comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        $(".comment-item[data-comment-id='" + d.comment.ParentCommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        return;
                    }
                });
            }
        });

        $rootScope.$on(configProvider.getSocketClientFunctions().wsConnect, function () {
            postsService.subscribeToPost($scope.post.Id);
        });

        $scope.getComments();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "postsService", "commentsService", "userService", "errorService", "configProvider"];

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
