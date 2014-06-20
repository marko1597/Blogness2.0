ngComments.directive('commentsContainer', [function () {
    var ctrlFn = function ($scope) {
        $scope.showAddComment = false;

        $scope.toggleShowAddComment = function() {
            $scope.showAddComment = !$scope.showAddComment;
        };

        $scope.$on("hideAddComment", function () {
            $scope.showAddComment = false;
        });
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            postid: '=',
            poster: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesUrl + "comments/commentsContainer.html",
        controller: ctrlFn
    };
}]);
