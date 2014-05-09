ngShared.directive("fileUploadItem", [
    function() {
        return {
            restrict: 'EA',
            scope: { item: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesUrl + "shared/fileUploadItem.html"
        };
    }
]);