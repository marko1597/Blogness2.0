postsModule.directive('postTags', function () {
    var ctrlFn = function ($scope) {
        $scope.dropdown = [];

        _.each($scope.data.Tags, function (a) {
            var obj = {
                "text": "#" + a.TagName,
                "href": "#"
            };
            $scope.dropdown.push(obj);
        });
    };

    ctrlFn.$inject = ["$scope", "postsService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div class="post-tags" data-post-id={{data.PostId}}>' +
                '<button class="btn btn-primary" data-animation="am-flip-x" bs-dropdown="dropdown" data-placement="top">' +
                    '<span>Click to view tags</span>' +
                '</button>' +
            '</div>',
        controller: ctrlFn
    };
});
