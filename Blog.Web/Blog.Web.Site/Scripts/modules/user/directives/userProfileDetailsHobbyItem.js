ngUser.directive('userProfileDetailsHobbyItem', [function () {
    var ctrlFn = function ($scope, blockUiService, errorService, userService) {
        $scope.isEditing = false;
        $scope.error = {};
        $scope.hobbyNameStore = "";

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
                    errorService.displayErrorRedirect(response.Error);
                }

                $scope.isEditing = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.deleteHobby = function() {
            userService.deleteUserHobby($scope.hobby.HobbyId).then(function (response) {
                if (response.Error != null) {
                    errorService.displayErrorRedirect(response.Error);
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
    };
    ctrlFn.$inject = ["$scope", "blockUiService", "errorService", "userService"];

    return {
        restrict: 'EA',
        scope: {
            hobby: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsHobbyItem.html",
        controller: ctrlFn
    };
}]);
