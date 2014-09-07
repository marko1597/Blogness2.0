ngUser.directive('userProfileDetailsInfo', [function () {
    var ctrlFn = function ($scope, blockUiService, errorService, userService, localStorageService) {
        $scope.isEditing = false;
        $scope.userFullName = null;
        $scope.error = {};
        $scope.username = localStorageService.get("username");

        $scope.editDetails = function () {
            $scope.isEditing = true;
        };

        $scope.saveDetails = function () {
            blockUiService.blockIt();
            userService.updateUser($scope.user).then(function (response) {
                if (response.Error == null) {
                    delete response.Education;
                    delete response.Address;
                    delete response.Hobbies;

                    $scope.user = response;
                    $scope.userFullName = $scope.user.FirstName + " " + $scope.user.LastName;

                    blockUiService.unblockIt();
                    $scope.isEditing = false;
                } else {
                    errorService.displayErrorRedirect(response.Error);
                    blockUiService.unblockIt();
                    $scope.isEditing = false;
                }
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
                blockUiService.unblockIt();
            });
        };

        $scope.setModelStateErrors = function (error) {
            for (var name in error) {
                var tmp = name.toString().split('.')[1];
                $scope.error[tmp] = error[name][0];
            }
        };

        $scope.hasError = function (errorProperty) {
            if ($scope.error[errorProperty] == undefined) {
                return "";
            }
            return "has-error";
        };

        $scope.showButtons = function () {
            if ($scope.username == undefined || $scope.username == null || $scope.username == "") {
                return "hidden";
            } else {
                if ($scope.user == undefined) {
                    return "hidden";
                } else {
                    if ($scope.username !== $scope.user.UserName) {
                        return "hidden";
                    }
                    return "";
                }
            }
        };
    };
    ctrlFn.$inject = ["$scope", "blockUiService", "errorService", "userService", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsInfo.html",
        controller: ctrlFn
    };
}]);
