ngShared.directive("fileUploadItem", [
    function () {
        return {
            restrict: 'EA',
            scope: { item: '=', uploader: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesUrl + "shared/fileUploadItem.html"
        };
    }
]);