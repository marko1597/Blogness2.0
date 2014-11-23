ngPosts.directive('postRelatedItem', ["$templateCache", function ($templateCache) {
    return {
        restrict: 'EA',
        scope: { post: '=' },
        replace: true,
        template: $templateCache.get("posts/postRelatedItem.html")
    };
}]);
