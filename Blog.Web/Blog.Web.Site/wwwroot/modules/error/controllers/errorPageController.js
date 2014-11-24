ngError.controller('errorPageController', ["$scope", "errorService", "configProvider",
    function ($scope, errorService, configProvider) {
        $scope.errorImage = configProvider.getSettings().BlogRoot + "/wwwroot/css/images/error-pages/servererror_bg2.png";
        $scope.error = errorService.getError();
    }
]);