ngShared.directive("fileUpload", [
    function () {
        return {
            restrict: 'EA',
            scope: { uploader: '='},
            replace: true,
            templateUrl: window.blogConfiguration.templatesUrl + "shared/fileUpload.html"
        };
    }
]);