ngTags.directive('tagItem', [function () {
    var ctrlFn = function ($scope) {
        
    };
    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { tag: '=' },
        replace: true,
        template:
            '<div class="tag-item" data-tag-id="{{tag.TagId}}">' +
                '<i class="fa fa-tags"></i>' +
                '{{tag.TagName}}' +
            '</div>',
        controller: ctrlFn
    };
}]);
