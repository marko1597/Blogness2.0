ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "errorService", "authenticationService",
    function ($scope, $location, $rootScope, localStorageService, userService, errorService, authenticationService) {
        $scope.user = null;

        $scope.userFullName = null;

        $scope.username = null;

        $scope.init = function () {
            if ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") {
                $scope.username = localStorageService.get("username");

                if ($scope.username == undefined || $scope.username == null) {
                    errorService.displayErrorRedirect({ Message: "You are not logged in. Try logging in or maybe create an account and join us."});
                } else {
                    authenticationService.getUserInfo().then(function (response) {
                        if (response.Message == undefined || response.Message == null) {
                            $scope.getUserInfo();
                        } else {
                            errorService.displayError(response.Message);
                        }
                    });
                }
            } else {
                $scope.username = $rootScope.$stateParams.username;
                $scope.getUserInfo();
            }
        };
        
        $scope.getUserInfo = function () {
            userService.getUserInfo($scope.username).then(function(user) {
                if (user.Error == null) {
                    $scope.user = user;
                    $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
                    $rootScope.$broadcast("viewedUserLoaded", user);

                    delete user.Education;
                    delete user.Address;
                    delete user.Hobbies;
                } else {
                    errorService.displayError(user.Error);
                }
            }, function(err) {
                errorService.displayError(err);
            });
        };
        
        $rootScope.$on("userLoggedIn", function () {
            $scope.getUserInfo();
        });

        $scope.init();
    }
]);