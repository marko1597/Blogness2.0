ngUser.directive('userProfileDetailsHobbyItem', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, blockUiService, errorService, userService, localStorageService) {
        $scope.isEditing = false;
        $scope.error = {};
        $scope.hobbyNameStore = "";
        $scope.username = localStorageService.get("username");

        $scope.cancelEditing = function () {
            $scope.hobby.HobbyName = $scope.hobbyNameStore;
            $scope.isEditing = false;
        };

        $scope.editHobby = function () {
            $scope.hobbyNameStore = $scope.hobby.HobbyName;
            $scope.isEditing = true;
        };

        $scope.saveHobby = function () {
            userService.updateUserHobby($scope.hobby).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                }

                $scope.isEditing = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.deleteHobby = function() {
            userService.deleteUserHobby($scope.hobby.HobbyId).then(function (response) {
                if (response.Error != null) {
                    errorService.displayError(response.Error);
                }

                $scope.$emit("successDeletingUserHobby", $scope.hobby);
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.setModelStateErrors = function (error) {
            for (var name in error) {
                var tmp = name.toString().split('.')[1];
                $scope.error[tmp] = error[name][0];
            }
        };

        $scope.hasError = function () {
            if ($scope.error.HobbyName == undefined || $scope.error.HobbyName == "") {
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
            hobby: '=',
            user: '='
        },
        replace: true,
        template: $templateCache.get("user/userProfileDetailsHobbyItem.html"),
        controller: ctrlFn
    };
}]);
