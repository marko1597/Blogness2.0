ngTags.directive('tagItem', ["$templateCache", function ($templateCache) {
    var ctrlFn = function ($scope) {
        
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { tag: '=' },
        replace: true,
        template: $templateCache.get("tags/tagItem.html"),
        controller: ctrlFn
    };
}]);
