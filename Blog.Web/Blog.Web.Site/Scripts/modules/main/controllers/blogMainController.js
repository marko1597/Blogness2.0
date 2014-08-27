blog.controller('blogMainController', ["$scope", "$location", "$rootScope", "$log", "$window", "configProvider", "authenticationService", "userService",
    function ($scope, $location, $rootScope, $log, $window, configProvider, authenticationService, userService) {
        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            //$log.info("location changing from " + current + " to " + next);
        });

        $rootScope.$on("userLoggedIn", function (ev, data) {
            if (data.username) {
                $scope.getUserInfo(data.username);
                $window.location.href = configProvider.getSettings().BlogRoot;
            }
        });

        $scope.init = function () {
            authenticationService.getUserInfo().then(function (response) {
                if (response.Message == undefined || response.Message == null) {
                    $scope.getUserInfo(response.Email);
                }
            }, function () {
                authenticationService.logout();
            });
        };

        $scope.getUserInfo = function(username) {
            userService.getUserInfo(username).then(function (resp) {
                resp.FullName = resp.FirstName + " " + resp.LastName;
                $rootScope.$broadcast("loggedInUserInfo", resp);
            });
        };

        $scope.init();
    }
]);