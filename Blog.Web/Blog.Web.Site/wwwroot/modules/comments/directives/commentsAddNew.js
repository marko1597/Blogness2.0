ngComments.directive('commentsAddNew', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, commentsService, errorService) {
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
            if ($scope.comment.CommentMessage != "") {
                commentsService.addComment($scope.createCommentForAdding()).then(function(resp) {
                    if (resp.Error == undefined) {
                        $scope.comment.CommentMessage = "";
                        $scope.hideAddComment();
                    } else {
                        $scope.hasError = true;
                        errorService.displayError(resp.Error);
                    }
                }, function(e) {
                    errorService.displayError(e);
                });
            } else {
                $scope.hasError = true;
                errorService.displayError({ Message: "Your comment message is empty. Please don't be that stupid." });
            }
        };

        $scope.createCommentForAdding = function () {
            if ($scope.comment.ParentCommentId) {
                $scope.comment.PostId = $scope.parentpostid;
                return $scope.comment;
            }
            return $scope.comment;
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.comment.User = $rootScope.user;
            }
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "commentsService", "errorService"];

    return {
        restrict: 'EA',
        scope: {
            commentid: '=',
            postid: '=',
            user: '=',
            parentpostid: '='
        },
        replace: true,
        template: $templateCache.get("comments/commentsAddNew.html"),
        controller: ctrlFn
    };
}]);
