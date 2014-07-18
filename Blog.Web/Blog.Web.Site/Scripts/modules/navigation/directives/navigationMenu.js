ngNavigation.directive('navigationMenu', function () {
    var ctrlFn = function ($scope, $rootScope, userService, configProvider, localStorageService) {
        $scope.navigationItems = configProvider.getNavigationItems();
        $scope.user = {};
        $scope.userFullName = "";
        $scope.authData = localStorageService.get("authorizationData");

        $scope.isLoggedIn = function() {
            if ($scope.authData) {
                return true;
            }
            return false;
        };

        $scope.launchLoginForm = function() {
            $rootScope.$broadcast("launchLoginForm", { canClose: true });
        };
       
        $scope.getUserInfo = function () {
            var username = localStorageService.get("username");
            if (username) {
                userService.getUserInfo(username).then(function (resp) {
                    $scope.user = resp;
                    $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
                });
            }
        };

        $scope.toggleNavigation = function() {
            $rootScope.$broadcast("toggleNavigation", { direction: 'left' });
        };

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.getUserInfo();
        });

        $scope.init = function() {
            if ($scope.authData) {
                $scope.getUserInfo();
            }
        };

        $scope.init();
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "userService", "configProvider", "localStorageService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "navigation/navigationMenu.html",
        controller: ctrlFn
    };
});
