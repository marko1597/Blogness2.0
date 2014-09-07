ngUser.directive('userProfileDetailsEducationItem', [function () {
    var ctrlFn = function ($scope, userService, dateHelper, blockUiService, errorService) {
        $scope.isEditing = $scope.isAdding == undefined ? false : ($scope.isAdding === "false" ? false : true);

        $scope.editEducation = function () {
            $scope.isEditing = true;
        };

        $scope.cancelEditing = function () {
            var isAdding = $scope.isAdding == undefined ? false : ($scope.isAdding === "false" ? false : true);
            if (isAdding) {
                $scope.$emit("cancelAddingUserEducation");
                return;
            }
            $scope.isEditing = false;
        };

        $scope.saveEducation = function () {
            var isAdding = $scope.isAdding == undefined ? false : ($scope.isAdding === "false" ? false : true);
            if (isAdding) {
                $scope.addEducation();
            } else {
                $scope.updateEducation();
            }
        };

        $scope.deleteEducation = function () {
            userService.deleteUserEducation($scope.education.EducationId).then(function (response) {
                if (response.Error != null) {
                    errorService.displayErrorRedirect(response.Error);
                }

                $scope.$emit("successDeletingUserEducation", $scope.education);
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.addEducation = function () {
            $scope.education.UserId = $scope.user.Id;

            userService.addUserEducation($scope.education).then(function (response) {
                if (response.Error != null) {
                    errorService.displayErrorRedirect(response.Error);
                }

                response.YearAttendedDisplay = dateHelper.getMonthYear(response.YearAttended);
                response.YearGraduatedDisplay = dateHelper.getMonthYear(response.YearGraduated);

                $scope.$emit("successAddingUserEducation", response);
                $scope.$emit("cancelAddingUserEducation");
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.updateEducation = function () {
            userService.updateUserEducation($scope.education).then(function (response) {
                if (response.Error != null) {
                    errorService.displayErrorRedirect(response.Error);
                }

                $scope.isEditing = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };
    };
    ctrlFn.$inject = ["$scope", "userService", "dateHelper", "blockUiService", "errorService"];

    return {
        restrict: 'EA',
        scope: {
            education: '=',
            isAdding: '=',
            user: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsEducationItem.html",
        controller: ctrlFn
    };
}]);
