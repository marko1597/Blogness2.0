blogEmpty.directive("emptyRecordMessage", ["$templateCache",
    function ($templateCache) {
        var ctrlFn = function ($scope) {
        };
        ctrlFn.$inject = ["$scope"];

        return {
            restrict: 'EA',
            scope: { message: '=' },
            replace: true,
            template: $templateCache.get("empty/emptyRecordMessage.html"),
            controller: ctrlFn
        };
    }
]);