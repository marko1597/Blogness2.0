ngTags.directive('tagItem', [function () {
    var ctrlFn = function ($scope) {
        
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { tag: '=' },
        replace: true,
        template:
            '<div data-tag-id="{{tag.TagId}}">' +
                '<span>{{tag.TaName}}</span>' +
            '</div>',
        controller: ctrlFn
    };
}]);
