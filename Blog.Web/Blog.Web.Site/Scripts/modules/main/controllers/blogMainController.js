blog.controller('blogMainController', ["$scope", "$location", "$rootScope", "$log", "localStorageService", "userService", "authenticationService",
    function ($scope, $location, $rootScope, $log, localStorageService, userService, authenticationService) {
        $scope.authData = localStorageService.get('authorizationData');

        $scope.username = null;

        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            $log.info("location changing from " + current + " to " + next);
            if ($rootScope.user) {
                $rootScope.$broadcast("loggedInUserInfo", $rootScope.user);
            }
        });

        $scope.init = function() {
            if ($scope.authData != null) {
                $scope.username = localStorageService.get('username');

                authenticationService.getUserInfo().then(function(response) {
                    if (response.Message == undefined || response.Message == null) {
                        $scope.getUserInfo($scope.username);
                    }
                }, function() {
                    authenticationService.logout();
                });
            } else {
                authenticationService.logout();
            }
        };

        $scope.getUserInfo = function (username) {
            userService.getUserInfo(username).then(function (user) {
                if (user.Error == null) {
                    $rootScope.$broadcast("loggedInUserInfo", user);
                    $rootScope.user = user;
                }
            });
        };

        $rootScope.$on("userLoggedIn", function (ev, data) {
            $scope.getUserInfo(data.username);
        });

        $scope.init();
    }
]);