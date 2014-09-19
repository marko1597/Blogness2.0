blog.controller('blogMainController', ["$scope", "$location", "$rootScope", "$log", "localStorageService", "userService", "authenticationService",
    function ($scope, $location, $rootScope, $log, localStorageService, userService, authenticationService) {
        $scope.authData = localStorageService.get('authorizationData');

        $scope.username = null;

        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            $log.info("location changing from " + current + " to " + next);
        });

        $scope.init = function() {
            if ($scope.authData != null) {
                $scope.username = localStorageService.get('username');

                authenticationService.getUserInfo().then(function(response) {
                    if (response.Message == undefined || response.Message == null) {
                        userService.getUserInfo($scope.username).then(function(user) {
                            if (user.Error == null) {
                                $rootScope.$broadcast("loggedInUserInfo", user);
                            }
                        });
                    }
                }, function() {
                    authenticationService.logout();
                });
            } else {
                authenticationService.logout();
            }
        };

        $scope.init();
    }
]);