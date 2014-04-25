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
                '<div class="post-item-header">' +
                    '<h4>{{data.PostTitle}}</h4>' +
                '</div>' +
                '<div class="post-item-body">' +
                    '<p>{{data.PostMessage}}</p>' +
                    '<div class="post-item-contents">' +
                        '<div ng-repeat="content in data.PostContents">' +
                            '<div class="post-item-content">' +
                                '<img ng-src="{{content.Media.MediaUrl}}" />' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>',
        controller: ctrlFn
    };
});
