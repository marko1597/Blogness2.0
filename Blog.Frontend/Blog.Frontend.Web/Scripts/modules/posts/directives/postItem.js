postsModule.directive('postItem', function () {
    var ctrlFn = function ($scope, $postsService) {
        //$scope.post = data;
    };
    ctrlFn.$inject = ["$scope", "postsService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div id="post-item-{{data.PostId}}">' +
                '<h4>{{data.PostTitle}}</h4>' +
                '<p>{{data.PostMessage}}</p>' +
            '</div>',
        controller: ctrlFn
    };
});
