ngShared.directive("emptyRecordMessage", [
    function () {
        var ctrlFn = function ($scope) {
            console.log($scope.message);
        };
        ctrlFn.$inject = ["$scope"];

        return {
            restrict: 'EA',
            scope: { message: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "shared/emptyRecordMessage.html",
            controller: ctrlFn
        };
    }
]);