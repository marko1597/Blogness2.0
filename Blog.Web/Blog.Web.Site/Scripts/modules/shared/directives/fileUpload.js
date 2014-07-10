ngShared.directive("fileUpload", [
    function () {
        return {
            restrict: 'EA',
            scope: { uploader: '='},
            replace: true,
            templateUrl: window.blogConfiguration.templatesModulesUrl + "shared/fileUpload.html"
        };
    }
]);