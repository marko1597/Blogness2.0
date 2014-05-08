ngPosts.directive("postContentUploadItem", [
    function() {
        return {
            restrict: 'EA',
            scope: { item: '=' },
            replace: true,
            templateUrl: window.blogConfiguration.templatesUrl + "posts/postContentUploadItem.html"
        };
    }
]);