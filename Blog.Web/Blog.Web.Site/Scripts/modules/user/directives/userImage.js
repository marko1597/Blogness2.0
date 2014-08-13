ngUser.directive('userImage', [function () {
    var ctrlFn = function ($scope) {
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: {
            user: '=',
            fullname: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userImage.html",
        controller: ctrlFn
    };
}]);
