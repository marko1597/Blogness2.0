ngPosts.directive('postLikes', function () {
    var ctrlFn = function ($scope) {
        $scope.tooltip = {
            "title": "Click to favorite this post.",
        };
    };
    ctrlFn.$inject = ["$scope", "postsService"];
    
    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div class="post-likes" data-post-id={{data.PostId}}>' +
                '<div class="wrapper" data-placement="right" data-type="info" data-animation="am-flip-x" bs-tooltip="tooltip">' +
                '<span><i class="fa fa-star-o"></i></span>' +
                '<span>{{data.PostLikes.length}}</span>' +
                '</div>' +
            '</div>',
        controller: ctrlFn
    };
});
