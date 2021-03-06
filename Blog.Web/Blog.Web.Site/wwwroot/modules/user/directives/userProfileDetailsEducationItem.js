﻿ngUser.directive('userProfileDetailsEducationItem', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, userService, dateHelper, blockUiService, errorService, localStorageService) {
        $scope.username = localStorageService.get("username");

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
                    errorService.displayError(response.Error);
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
                    errorService.displayError(response.Error);
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
                    errorService.displayError(response.Error);
                }

                $scope.isEditing = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
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

        $scope.educationCourseDisplay = function() {
            if (!$scope.education.Course || $scope.education.Course === '') {
                return 'No course selected';
            } else {
                return $scope.education.Course;
            }
        };
    };
    ctrlFn.$inject = ["$scope", "userService", "dateHelper", "blockUiService", "errorService", "localStorageService"];

    return {
        restrict: 'EA',
        scope: {
            education: '=',
            isAdding: '=',
            user: '='
        },
        replace: true,
        template: $templateCache.get("user/userProfileDetailsEducationItem.html"),
        controller: ctrlFn
    };
}]);
