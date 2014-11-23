ngUser.directive('userProfileDetails', ["$templateCache", function ($templateCache) {
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
        template: $templateCache.get("user/userProfileDetails.html"),
        controller: ctrlFn
    };
}]);
