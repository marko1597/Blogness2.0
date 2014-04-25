postsModule.directive('postContent', function () {
    var ctrlFn = function ($scope, $postsService) {
        $scope.posts = [];
        $scope.errorContent = { Show: false, Type: "" };

        $scope.getPopularPosts = function () {
            $postsService.getPopularPosts().then(function (resp) {
                $scope.posts = resp;
            }, function (errorMsg) {
                alert(errorMsg);
            });
        };

        $scope.loadMorePosts = function () {
            
        };

        $scope.getPopularPosts();
    };
    ctrlFn.$inject = ["$scope", "postsService"];

    return {
        restrict: 'EA',
        scope: { data: '=' },
        replace: true,
        template:
            '<div id="posts-main" isotope-container infinite-scroll="loadMorePosts()" infinite-scroll-distance="0">' +
                '<div ng-repeat="post in posts" isotope-item post-item></div>' +
            '</div>',
        controller: ctrlFn
    };
});
