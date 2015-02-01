blogFileUpload.directive("fileUpload", ["$templateCache",
    function ($templateCache) {
        return {
            restrict: 'EA',
            scope: { uploader: '='},
            replace: true,
            template: $templateCache.get("fileUpload/fileUpload.html")
        };
    }
]);