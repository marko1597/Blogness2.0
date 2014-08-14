ngUser.controller('userProfileController', ["$scope", "$location", "$rootScope", "localStorageService", "userService", "blockUiService", "errorService",
    function ($scope, $location, $rootScope, localStorageService, userService, blockUiService, errorService) {
        $scope.authData = localStorageService.get("authorizationData");
        $scope.user = null;
        $scope.userFullName = null;
        $scope.isEditing = false;
        $scope.username = ($rootScope.$stateParams.username == null || $rootScope.$stateParams.username === "undefined") ?
                localStorageService.get("username") : $rootScope.$stateParams.username;

        $scope.getUserInfo = function () {
            blockUiService.blockIt();

            if ($scope.username) {
                userService.getUserInfo($scope.username).then(function (response) {
                    if (response.Error == null) {
                        $scope.user = response;
                        $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;
                        blockUiService.unblockIt();
                    } else {
                        errorService.displayErrorRedirect(response.Error);
                        blockUiService.unblockIt();
                    }
                }, function (err) {
                    errorService.displayErrorRedirect(err);
                    blockUiService.unblockIt();
                });
            } else {
                errorService.displayErrorRedirect({ Message: "User lookup failed. Sorry. :(" });
                blockUiService.unblockIt();
            }
        };

        $rootScope.$on("userLoggedIn", function () {
            $scope.authData = localStorageService.get("authorizationData");
            $scope.getUserInfo();
        });

        $scope.getUserInfo();
    }
]);