ngShared.directive("fileUploadItem", [
    function () {
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
            templateUrl: window.blogConfiguration.templatesModulesUrl + "shared/fileUploadItem.html"
        };
    }
]);