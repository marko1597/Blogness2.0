ngComments.directive('commentsAddNew', [function () {
    var ctrlFn = function ($scope) {
        $scope.commentMessage = "";

        $scope.hideAddComment = function() {
            $scope.$emit("hideAddComment");
        };
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: {
            commentid: '=',
            postid: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentsAddNew.html",
        controller: ctrlFn
    };
}]);
