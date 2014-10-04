ngLogin.directive('loggedUser', function () {
    var ctrlFn = function ($scope, $rootScope, $location, $window, userService, configProvider, localStorageService, authenticationService) {
        $scope.user = {};

        $scope.authData = localStorageService.get("authorizationData");

        $scope.isLoggedIn = function () {
            if ($scope.authData) {
                return true;
            }
            return false;
        };

        $scope.goToProfile = function() {
            $location.path("/user");
        };

        $scope.logout = function () {
            authenticationService.logout();
            $window.location.href = configProvider.getSettings().BlogRoot + "/account";
        };

        $scope.launchLoginForm = function () {
            $rootScope.$broadcast("launchLoginForm", { canClose: true });
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
    ctrlFn.$inject = ["$scope", "$rootScope", "$location", "$window", "userService", "configProvider", "localStorageService", "authenticationService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        controller: ctrlFn,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "login/loggedUser.html",
    };
});
