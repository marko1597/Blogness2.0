ngNavigation.directive('navigationMenu', function () {
    var ctrlFn = function ($scope, $rootScope, userService, configProvider) {
        $scope.navigationItems = configProvider.getNavigationItems();
        $scope.user = {};
        $scope.userFullName = "";

        $scope.getUserInfo = function () {
            userService.getUserInfo().then(function(resp) {
                $scope.user = resp;
                $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
            });
        };

        $scope.toggleNavigation = function() {
            $rootScope.$broadcast("toggleNavigation", { direction: 'left' });
        };

        $scope.getUserInfo();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "userService", "configProvider"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "navigation/navigationMenu.html",
        controller: ctrlFn
    };
});
