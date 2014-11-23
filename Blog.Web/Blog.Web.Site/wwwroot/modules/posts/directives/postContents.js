ngPosts.directive('postContents', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope) {
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { contents: '=' },
        replace: true,
        template: $templateCache.get("posts/postContents.html"),
        controller: ctrlFn
    };
}]);
