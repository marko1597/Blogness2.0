ngComments.directive('commentsList', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $interval, postsService, commentsService, userService, errorService, configProvider) {
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

        var stop;
        stop = $interval(function () {
            if (configProvider.getSocketClientFunctions().commentAdded && configProvider.getSocketClientFunctions().wsConnect) {
                $rootScope.$on(configProvider.getSocketClientFunctions().commentAdded, function (e, d) {
                    d.comment = commentsService.addViewProperties(d.comment);

                    if (d.commentId !== null && d.commentId != undefined) {
                        var comment = _.where($scope.comments, { Id: d.commentId })[0];
                        if (comment.Comments === null) comment.Comments = [];

                        comment.Comments.unshift(d.comment);

                        $(".comment-item[data-comment-id='" + d.comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
                        $(".comment-item[data-comment-id='" + d.comment.ParentCommentId + "']").effect("highlight", { color: "#B3C833" }, 1500);
                    } else {
                        $scope.comments.unshift(d.comment);
                        $(".comment-item[data-comment-id='" + d.comment.Id + "']").effect("highlight", { color: "#B3C833" }, 1500);
                    }
                });

                $rootScope.$on(configProvider.getSocketClientFunctions().wsConnect, function () {
                    postsService.subscribeToPost($scope.postid);
                });

                $interval.cancel(stop);
                stop = undefined;
            }
        }, 250);

        $scope.getComments();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$interval", "postsService", "commentsService", "userService", "errorService", "configProvider"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            postid: '=',
            poster: '='
        },
        replace: true,
        template: $templateCache.get("comments/commentsList.html"),
        controller: ctrlFn
    };
}]);
