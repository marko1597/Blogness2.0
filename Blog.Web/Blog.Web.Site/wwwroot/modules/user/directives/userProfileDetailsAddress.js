ngUser.directive('userProfileDetailsAddress', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope, $rootScope, errorService, userService, localStorageService) {
        $scope.isEditing = false;

        $scope.address = {};

        $scope.error = {};

        $scope.username = localStorageService.get("username");

        $scope.editAddress = function () {
            $scope.isEditing = true;
        };

        $scope.cancelEditAddress = function () {
            $scope.isEditing = false;
        };

        $scope.saveAddress = function () {
            userService.updateUserAddress($scope.address).then(function (response) {
                if (response.Error == null) {
                    $scope.address = response;
                } else {
                    errorService.displayError(response.Error);
                }
                $scope.isEditing = false;
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

        $scope.$on("viewedUserLoaded", function (ev, data) {
            $scope.address = data.Address;
            $scope.user = data;
        });
    };
    ctrlFn.$inject = ["$scope", "$rootScope", "errorService", "userService", "localStorageService"];

    return {
        restrict: 'EA',
        replace: true,
        template: $templateCache.get("user/userProfileDetailsAddress.html"),
        controller: ctrlFn
    };
}]);
