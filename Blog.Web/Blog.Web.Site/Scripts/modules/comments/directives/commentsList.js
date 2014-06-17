ngComments.directive('commentsList', [function () {
    var ctrlFn = function ($scope) {
        $scope.$on("viewPostCommentsLoaded", function (e, d) {
            $scope.comments = d.Comments;
            $scope.user = d.User;
        });

        $scope.canExpandComment = function (comment) {
            if (comment.Comments == undefined || comment.Comments == null || comment.Comments.length < 1) {
                return "hidden";
            }
            return "";
        };

        $scope.canExpandComment = function (comment) {
            if (comment.PostId == undefined || comment.PostId == null) {
                return "hidden";
            }
            return "";
        };
    };
    ctrlFn.$inject = ["$scope"];
    
    return {
        restrict: 'EA',
        scope: {
            comments: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentsList.html",
        controller: ctrlFn
    };
}]);
