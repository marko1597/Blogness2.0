ngTags.directive('tagItem', [function () {
    var ctrlFn = function ($scope) {
        
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { tag: '=' },
        replace: true,
        templateUrl: window.blogConfiguration.templatesModulesUrl + "tags/tagItem.html",
        controller: ctrlFn
    };
}]);
