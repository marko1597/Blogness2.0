ngMedia.directive('mediaGroupedList', function () {
    var ctrlFn = function ($scope, $rootScope, albumService, localStorageService) {
        $scope.authData = localStorageService.get("authorizationData");
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "albumService", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            albums: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "media/mediaGroupedList.html",
        controller: ctrlFn
    };
});
