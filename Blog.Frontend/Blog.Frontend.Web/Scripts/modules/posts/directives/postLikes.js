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
        templateUrl: window.blogConfiguration.templatesUrl + "posts/postlikes.html",
        controller: ctrlFn
    };
});
