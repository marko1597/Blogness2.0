ngPosts.directive('postTags', function () {
    var ctrlFn = function ($scope) {
        
    };

    ctrlFn.$inject = ["$scope"];

    return {
        restrict: 'EA',
        scope: { tags: '=' },
        replace: true,
        template:
            '<div class="post-tags">' +
                '<span data-tag-id="{{tag.TagId}}" ng-repeat="tag in tags">' +
                    '<i class="fa fa-tags"></i>' +
                    '{{tag.TagName}}' +
                '</span>' +
            '</div>',
        controller: ctrlFn
    };
});
