ngUser.directive('userProfileDetailsAddress', [function () {
    var ctrlFn = function ($scope, blockUiService, errorService, userService) {
        $scope.isEditing = false;
        $scope.address = {};
        $scope.error = {};

        $scope.editAddress = function () {
            $scope.isEditing = true;
        };

        $scope.saveAddress = function () {
            userService.updateUserAddress($scope.address).then(function (response) {
                if (response.Error == null) {
                    $scope.address = response;
                    blockUiService.unblockIt();
                } else {
                    errorService.displayErrorRedirect(response.Error);
                    blockUiService.unblockIt();
                }
                $scope.isEditing = false;
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
    };
    ctrlFn.$inject = ["$scope", "blockUiService", "errorService", "userService"];

    return {
        restrict: 'EA',
        scope: {
            address: '='
        },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "user/userProfileDetailsAddress.html",
        controller: ctrlFn
    };
}]);
