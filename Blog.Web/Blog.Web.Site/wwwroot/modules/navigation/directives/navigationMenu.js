ngNavigation.directive('navigationMenu', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, $window, userService, configProvider, localStorageService, authenticationService) {
        $scope.navigationItems = configProvider.getNavigationItems();

        $scope.user = {};

        $scope.authData = localStorageService.get("authorizationData");

        $scope.isLoggedIn = function() {
            if ($scope.authData) {
                return true;
            }
            return false;
        };

        $scope.logout = function () {
            authenticationService.logout();
            $window.location.href = configProvider.getSettings().BlogRoot + "/account";
        };
        
        $scope.launchLoginForm = function() {
            $rootScope.$broadcast("launchLoginForm", { canClose: true });
        };
        
        $scope.toggleNavigation = function() {
            $rootScope.$broadcast("toggleNavigation", { direction: 'left' });
        };

        $rootScope.$watch('user', function () {
            if ($rootScope.user) {
                $scope.user = $rootScope.user;
                $scope.user.FullName = $scope.user.FirstName + " " + $scope.user.LastName;
            }
        });

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "$window", "userService", "configProvider", "localStorageService", "authenticationService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template: $templateCache.get("navigation/navigationMenu.html"),
        controller: ctrlFn
    };
}]);
