blogFileUpload.directive("fileUploadItem", ["$templateCache",
    function ($templateCache) {
        var linkFn = function(scope) {
            scope.isNewContent = function (exists) {
                var response = true;
                if (exists) {
                    response = false;
                }
                return response;
            };
        };

        return {
            link: linkFn,
            restrict: 'EA',
            scope: {
                item: '=',
                uploader: '='
            },
            replace: true,
            template: $templateCache.get("fileUpload/fileUploadItem.html")
        };
    }
]);