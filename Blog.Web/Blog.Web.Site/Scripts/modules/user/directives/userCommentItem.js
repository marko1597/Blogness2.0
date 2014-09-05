ngUser.directive('userCommentItem', [function () {
    var ctrlFn = function ($scope) {
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { comment: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userCommentItem.html",
        controller: ctrlFn
    };
}]);
