ngUser.directive('userProfileDetailsHobbies', [function () {
    var ctrlFn = function ($scope, $rootScope, blockUiService, errorService, userService, localStorageService) {
        $scope.isAdding = false;

        $scope.hobbies = [];

        $scope.error = {};

        $scope.newHobby = { HobbyName: "" };

        $scope.emptyRecordMessage = "Uhhh..a no lifer..";

        $scope.username = localStorageService.get("username");

        $scope.addHobby = function () {
            $scope.isAdding = true;
        };

        $scope.cancelAdding = function () {
            $scope.isAdding = false;
        };

        $scope.saveHobby = function () {
            $scope.newHobby.UserId = $scope.user.Id;

            userService.addUserHobby($scope.newHobby).then(function (response) {
                if (response.Error == null) {
                    $scope.hobbies.push(response);
                    $scope.newHobby = { HobbyName: "", UserId: $scope.user };
                } else {
                    errorService.displayError(response.Error);
                }

                $scope.isAdding = false;
            }, function (err) {
                $scope.setModelStateErrors(err.ModelState);
            });
        };

        $scope.$on("successDeletingUserHobby", function (ev, data) {
            var hobbyIndex = $scope.hobbies.indexOf(data);
            $scope.hobbies.splice(hobbyIndex, 1);
        });

        $scope.showNoRecordsMessage = function () {
            if ($scope.hobbies.length > 0) {
                return false;
            }
            return true;
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

        $rootScope.$on("viewedUserLoaded", function(ev, data) {
            $scope.hobbies = data.Hobbies;
            $scope.user = data;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "blockUiService", "errorService", "userService", "localStorageService"];

    return {
        restrict: 'EA',
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsHobbies.html",
        controller: ctrlFn
    };
}]);
