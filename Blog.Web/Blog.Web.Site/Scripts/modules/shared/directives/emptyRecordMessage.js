ngShared.directive("emptyRecordMessage", [
    function () {
        var ctrlFn = function ($scope) {
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