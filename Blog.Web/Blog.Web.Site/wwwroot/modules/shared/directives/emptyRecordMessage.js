ngShared.directive("emptyRecordMessage", ["$templateCache",
    function ($templateCache) {
        var ctrlFn = function ($scope) {
        };
        ctrlFn.$inject = ["$scope"];

        return {
            restrict: 'EA',
            scope: { message: '=' },
            replace: true,
            template: $templateCache.get("shared/emptyRecordMessage.html"),
            controller: ctrlFn
        };
    }
]);