ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "blockUiService", "errorService",
    function ($scope, $location, $rootScope, localStorageService, userService, blockUiService, errorService) {
        $scope.authData = localStorageService.get("authorizationData");
        $scope.user = null;
        $scope.userFullName = null;
        $scope.isEditing = false;

        $scope.getUserInfo = function () {
            if ($scope.authData) {
                var username = localStorageService.get("username");
                if (username) {
                    userService.getUserInfo(username).then(function(resp) {
                        $scope.user = resp;
                        $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
                    }, function(err) {
                        errorService.displayErrorRedirect(err);
                    });
                }
            }
        };

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.getUserInfo();
        });

        $scope.getUserInfo();
    }
]);