ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "blockUiService", "errorService",
    function ($scope, $location, $rootScope, localStorageService, userService, blockUiService, errorService) {
        $scope.authData = localStorageService.get("authorizationData");
        $scope.user = null;
        $scope.userFullName = null;
        $scope.isEditing = false;
        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.getUserInfo = function () {
            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (resp) {
                    $scope.user = resp;
                    $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;

                    //$rootScope.$state.go(($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined")
                    //    ? 'ownprofile.details' : 'othersprofile.details');
                }, function(err) {
                    errorService.displayErrorRedirect(err);
                });
            } else {
                errorService.displayErrorRedirect({ Message: "User lookup failed. Sorry. :(" });
            }
        };

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.getUserInfo();
        });

        $scope.getUserInfo();
    }
]);