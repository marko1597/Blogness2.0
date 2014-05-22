ngError.controller('errorPageController', ["$scope", "errorService",
    function ($scope, errorService) {
        $scope.error = errorService.getError();
    }
]);